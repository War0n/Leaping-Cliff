using Barometer_ASP_NET.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;
using System.Diagnostics;

namespace Barometer_ASP_NET.Controllers
{
    public class BaroController : Controller
    {
        /// <summary>
        /// Database
        /// </summary>
        private DatabaseClassesDataContext _db = DatabaseFactory.getInstance().getDataContext();

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
        /// Bij het selecteren van de report date wordt er een request gemaakt naar de Widget()
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

    }
}
