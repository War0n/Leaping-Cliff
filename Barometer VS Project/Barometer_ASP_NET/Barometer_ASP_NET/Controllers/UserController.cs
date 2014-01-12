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
            UserDashboardWrapper wrapper = new UserDashboardWrapper(CurrentUser.getInstance().Studentnummer);
            return View(wrapper);
        }

		public ActionResult Barometer()
		{
			return View();
		}

		public ActionResult Project()
		{
            UserProjectWrapper wrapper = new UserProjectWrapper(CurrentUser.getInstance().Studentnummer); //IMPORTANT, uses a test value
            return View(wrapper);
		}

		public ActionResult Projecten()
		{
            UserDashboardWrapper wrapper = new UserDashboardWrapper(CurrentUser.getInstance().Studentnummer);
			return View(wrapper);
		}
    }
}
