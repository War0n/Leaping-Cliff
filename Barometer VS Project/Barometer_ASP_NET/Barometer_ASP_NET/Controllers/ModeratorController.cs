using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Controllers
{
    public class ModeratorController : Controller
    {
        //
        // GET: /Moderator/

        public ActionResult Dashboard()
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

    }
}
