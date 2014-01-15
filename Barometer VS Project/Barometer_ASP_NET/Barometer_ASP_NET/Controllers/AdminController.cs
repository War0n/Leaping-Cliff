using Barometer_ASP_NET.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Controllers
{
    public class AdminController : Controller
    {
        AdminDashboardWrapper wrapper = new AdminDashboardWrapper(3242344);
        AdminProjectDetailWrapper pwrapper = new AdminProjectDetailWrapper(3242344);
        //
        // GET: /Admin/

        public ActionResult Dashboard()
        {
            return View(wrapper);
        }

		public ActionResult Barotemplate()
		{
			return View();
		}

		public ActionResult Create()
		{
			return View();
		}

		public ActionResult Detail()
		{
			return View();
		}

		public ActionResult List()
		{
			return View(wrapper);
		}

		public ActionResult ProjectGroups()
		{
			return View(pwrapper);
		}

		public ActionResult ProjectGroupsView()
		{
			return View();
		}

        public ActionResult Student(int studentId)
        {
            BarometerDataAccesLayer.DatabaseClassesDataContext context = Database.DatabaseFactory.getInstance().getDataContext();
            var student =
                from u in context.Users
                where u.id == studentId
                select u;
            return View(student.First());
        }

        public ActionResult StudentForm(FormCollection collection)
        {
            int studentNumber = int.Parse(collection.GetValue("student").AttemptedValue);
            BarometerDataAccesLayer.DatabaseClassesDataContext context = Database.DatabaseFactory.getInstance().getDataContext();
            var student =
                from u in context.Users
                where u.student_number == studentNumber
                select u.id;
            int studentId = student.First();
            return RedirectToAction("Student",new {studentId = studentId});
        }

        public ActionResult DeleteProject(int projectId)
        {
            BarometerDataAccesLayer.DatabaseClassesDataContext context = Database.DatabaseFactory.getInstance().getDataContext();
            var resultProjects=
                from p in context.Projects
                where p.id == projectId
                select p;

            context.Projects.DeleteOnSubmit(resultProjects.First());
            context.SubmitChanges();

            return RedirectToAction("List");
        }
    }
}
