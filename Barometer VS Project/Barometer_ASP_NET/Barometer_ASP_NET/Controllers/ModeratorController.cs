using Barometer_ASP_NET.Wrappers;
using BarometerDataAccesLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Controllers
{
    public class ModeratorController : Controller
    {
        ModeratorDashboardWrapper wrapper = new ModeratorDashboardWrapper(CurrentUser.getInstance().Studentnummer); 
        //
        // GET: /Moderator/

        public ActionResult Dashboard()
        {
            return View(wrapper);
        }

        public ActionResult StudentForm(FormCollection collection)
        {
            int studentNumber = int.Parse(collection.GetValue("student").AttemptedValue);
            BarometerDataAccesLayer.DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            var student =
                from u in context.Users
                where u.student_number == studentNumber
                select u.id;
            int studentId = student.First();
            return RedirectToAction("Student","Admin", new { studentId = studentId });
        }

    }
}
