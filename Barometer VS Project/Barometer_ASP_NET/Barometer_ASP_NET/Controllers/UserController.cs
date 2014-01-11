using Barometer_ASP_NET.Database;
using Barometer_ASP_NET.Wrappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public DAOStudent student = new DAOStudent();
        public DAOProject project = new DAOProject();

        public ActionResult Dashboard()
        {
            UserDashboardWrapper wrapper = new UserDashboardWrapper(2000000);
            return View(wrapper);
        }

		public ActionResult Barometer()
		{
			return View();
		}

		public ActionResult Project()
		{

            UserProjectWrapper wrapper = new UserProjectWrapper(3000000); //IMPORTANT, uses a test value
            return View(wrapper);
		}

		public ActionResult Projecten()
		{
            UserDashboardWrapper wrapper = new UserDashboardWrapper(2000000);
            return View(wrapper);
		}
    }
}
