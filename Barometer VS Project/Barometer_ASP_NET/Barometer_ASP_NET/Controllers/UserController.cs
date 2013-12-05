using System;
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

        public ActionResult Dashboard()
        {
            return View();
        }

		public ActionResult Barometer()
		{
			return View();
		}

		public ActionResult Project()
		{
			return View();
		}

		public ActionResult Projecten()
		{
			return View();
		}
    }
}
