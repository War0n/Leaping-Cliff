using Barometer_ASP_NET.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;
using System.Diagnostics;
using Barometer_ASP_NET.Filters;

namespace Barometer_ASP_NET.Controllers
{
    public class BaroController : Controller
    {
        /// <summary>
        /// Database
        /// </summary>
        private DatabaseClassesDataContext _db = new DatabaseClassesDataContext();


        /// <summary>
        /// Verkrijg barometer invul form
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(int reporter_id = 0, int subject_id = 0, int project_report_date_id = 0)
        {
            BaroReportWrapper viewModel =
                _db.ProjectReportDates
                .Where(r => r.id == project_report_date_id)
                .Select(r => new BaroReportWrapper
                {
                    ReportDate = r,
                    Project = r.Project,
                    Aspects = r.Project.BaroTemplate.BaroAspects.Where(a => a.is_head_aspect == 1).ToList()
                }).FirstOrDefault();

            ViewBag.reporter_id = reporter_id;
            ViewBag.subject_id = subject_id;

            return PartialView("_BaroFormPartial", viewModel);
        }

        /// <summary>
        /// Submit en verwerk barometer form
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Dictionary<int, int> reports = new Dictionary<int, int>();

            // Parse alle cijfers naar reports (Dit kan makkelijker maar kon zo snel ff niet vinden hoe
            foreach (var key in form.AllKeys)
            {
                if (key.StartsWith("aspect[") && key.EndsWith("]"))
                {
                    int aspect_id = Convert.ToInt32(key.Split(new char[] { '[', ']' })[1]);
                    if (form[key] != null && form[key] != "")
                    {
                        reports.Add(aspect_id, Convert.ToInt32(form[key]));
                    }
                    else
                    {
                        reports.Add(aspect_id, 1);
                    }
                    
                }
            }

            // Vull alle beoordeling naar database door
            foreach (KeyValuePair<int, int> entry in reports)
            {
                Report r = new Report();
                r.reporter_id = Convert.ToInt32(form.GetValue("reporter_id").AttemptedValue);
                r.subject_id = Convert.ToInt32(form.GetValue("subject_id").AttemptedValue);
                r.project_report_date_id = Convert.ToInt32(form.GetValue("project_report_date_id").AttemptedValue);
                r.baro_aspect_id = entry.Key;
                r.grade = entry.Value;
                _db.Reports.InsertOnSubmit(r);
            }
            _db.SubmitChanges();


            return Json(new { status = "success", subject_id = Convert.ToInt32(form.GetValue("subject_id").AttemptedValue)});
        }

        /// <summary>
        /// Maak template aan
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult CreateTemplate(string name = "")
        {
            if(!(name.Length > 0)) {
                return Json(new { status = "error"}, JsonRequestBehavior.AllowGet);
            }


            BaroTemplate newTemplate = new BaroTemplate();
            newTemplate.template_name = name;
            newTemplate.rating_type = 0;
            newTemplate.anonymous = 0;

            _db.BaroTemplates.InsertOnSubmit(newTemplate);
            _db.SubmitChanges();


            return Json(new {status = "success", baro_template_id = newTemplate.id}, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Voeg een aspect toe en retourneer een nieuwe tree
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAspect(BaroAspect aspect)
        {
            aspect.parent_id = (aspect.parent_id == 0) ? null : aspect.parent_id;
            if (aspect.parent_id == null) // Als node geen parent heeft is het een head aspect
            {
                aspect.is_head_aspect = 1;
            }

            _db.BaroAspects.InsertOnSubmit(aspect);
            _db.SubmitChanges();

            // Haal de tree op
            BaroTreeViewWrapper viewModel = _db.BaroTemplates.Where(r => r.id == aspect.baro_template_id)
                                            .Select(r => new BaroTreeViewWrapper
                                            {
                                                Template = r,
                                                HeadAspect = r.BaroAspects.Where(d => d.is_head_aspect == 1 && d.parent_id == null || d.parent_id == 0).ToList()
                                            }).FirstOrDefault();


            string treeHtml = RazorViewToString.RenderRazorViewToString(this, "_BaroTreePartial", viewModel);
            ViewBag.parent_id = aspect.parent_id;
            string selectHtml = RazorViewToString.RenderRazorViewToString(this, "_BaroSelectParents", viewModel);

            return Json(new { status = "success", html_tree = treeHtml, html_select = selectHtml}, JsonRequestBehavior.AllowGet);
        }






        /// <summary>
        /// Toon een report card. Wordt alleen aangeroepen vanaf de Holder.
        /// </summary>
        /// <param name="reporter_id">Reporter</param>
        /// <param name="subject_id">Subject</param>
        /// <param name="project_id">Project</param>
        /// <param name="project_report_date_id">Report date</param>
        /// <returns></returns>
        public ActionResult Widget(int reporter_id = 0, int subject_id = 0, int project_id = 0, int project_report_date_id = 0)
        {
            if (!Request.IsAjaxRequest())
                return HttpNotFound();

            BaroWidgetWrapper viewModel = (from p in _db.Projects
                          where (p.id == project_id)
                          select p).Select(r => new BaroWidgetWrapper{
                                Project = r,
                                ReportDates = r.ProjectReportDates.ToList(),
                                HeadAspects = r.BaroTemplate.BaroAspects.Where(d => d.is_head_aspect == 1).ToList()
                          }).FirstOrDefault();
            viewModel.Subject = _db.Users.Where(r => r.id == subject_id).FirstOrDefault();
            viewModel.Reporter = _db.Users.Where(r => r.id == reporter_id).FirstOrDefault();

            ViewBag.report_date_id = project_report_date_id;

            return PartialView("_BaroWidget", viewModel);
        }


        /// <summary>
        /// Haal de holder op voor het bekijken van de beoordelingen.
        /// Deze weergeeft de SELECT met daarin de report dates. 
        /// Bij het selecteren van de report date wordt er een request gemaakt naar de Widget() die dan de reports laat zien
        /// </summary>
        /// <param name="reporter_id"></param>
        /// <param name="subject_id"></param>
        /// <param name="project_id"></param>
        /// <returns></returns>
        public ActionResult WidgetHolder(int reporter_id = 0, int subject_id = 0, int project_id = 0)
        {
            if (!Request.IsAjaxRequest())
                return HttpNotFound();


            // Maak de viewmodel aan
            BaroWidgetWrapper viewModel = (from p in _db.Projects
                                           where (p.id == project_id)
                                           select p).Select(r => new BaroWidgetWrapper
                                           {
                                               Project = r,
                                               ReportDates = r.ProjectReportDates.ToList()
                                           }).FirstOrDefault();

            viewModel.Subject = _db.Users.Where(r => r.id == subject_id || r.student_number == subject_id).FirstOrDefault();
            viewModel.Reporter = _db.Users.Where(r => r.id == reporter_id || r.student_number == reporter_id).FirstOrDefault();

            return PartialView("_BaroWidgetHolder", viewModel);
        }

        /// <summary>
        /// TEST METHOD om te kijken hoe een baro mmeter er uit ziet
        /// </summary>
        /// <param name="template_id"></param>
        /// <returns></returns>
        public ActionResult GetTree(int template_id = 0)
        {
            BaroTreeViewWrapper viewModel = _db.BaroTemplates.Where(r => r.id == template_id)
                                           .Select(r => new BaroTreeViewWrapper
                                           {
                                               Template = r,
                                               HeadAspect = r.BaroAspects.Where(d => d.is_head_aspect == 1 && d.parent_id == null || d.parent_id == 0).ToList()
                                           }).FirstOrDefault();


            return PartialView("_BaroTreePartial", viewModel);
        }


        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

    }
}
