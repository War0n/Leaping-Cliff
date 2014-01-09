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
        //
        // GET: /Admin/

        public ActionResult Dashboard()
        {
            AdminDashboardWrapper wrapper = new AdminDashboardWrapper(3242344);
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
			return View();
		}

		public ActionResult ProjectGroups()
		{
			return View();
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
    }
}
