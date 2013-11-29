using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Areas.admin.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /admin/Dashboard/

        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
