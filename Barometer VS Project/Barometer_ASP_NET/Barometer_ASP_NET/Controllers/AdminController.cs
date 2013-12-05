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
            return View();
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

    }
}
