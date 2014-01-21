using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;
using Barometer_ASP_NET.FileFactory;
using Barometer_ASP_NET.Wrappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barometer_ASP_NET.Filters;

namespace Barometer_ASP_NET.Controllers
{
    public class UserController : Controller
    {
        private DatabaseClassesDataContext db = new DatabaseClassesDataContext();
        private DAOStudent daoStudent = new DAOStudent();
        private DAOProject daoProject = new DAOProject();
        private CurrentUser curUser = CurrentUser.getInstance();

        
        [AuthFilter("all")]
        public ActionResult Dashboard()
        {
            //VOOR ALS JE WILT TESTEN WAT JE STUDENTNUM IS : 
            // return Content(CurrentUser.getInstance().Studentnummer.ToString());

            UserDashboardWrapper wrapper = new UserDashboardWrapper(curUser.Studentnummer);

            ViewBag.Filled = (Request["b"] == "done") ? true : false;

            return View(wrapper);
        }

        [AuthFilter("user")]
		public ActionResult Barometer(int report_id = 0)
		{
            BaroReportWrapper viewModel =
                db.ProjectReportDates
                .Where(r => r.id == report_id)
                .Select(r => new BaroReportWrapper
                {
                    ReportDate = r,
                    Project = r.Project,
                    Aspects = r.Project.BaroTemplate.BaroAspects.Where(a => a.is_head_aspect == 1).ToList()
                }).FirstOrDefault();

            if (viewModel == null)
                return HttpNotFound();

            ProjectGroup group = daoStudent.getStudentGroup(curUser.Studentnummer).First();
            if (group == null)
                return RedirectToAction("Dashboard");


            viewModel.ReporterId = curUser.StudentId;
            viewModel.Members = daoProject.getUsersInGroup(group.id).ToList();
            

			return View(viewModel);
		}

       
        [AuthFilter("all")]
        public ActionResult Project(int project_id = 0)
		{
            if (project_id == 0)
                return RedirectToAction("Projecten");
            UserProjectWrapper wrapper = new UserProjectWrapper(curUser.Studentnummer, project_id); 
            return View(wrapper);
		}

       
        [AuthFilter("all")]
		public ActionResult Projecten()
		{
            UserDashboardWrapper wrapper = new UserDashboardWrapper(curUser.Studentnummer);
			return View(wrapper);
		}
    }
}
