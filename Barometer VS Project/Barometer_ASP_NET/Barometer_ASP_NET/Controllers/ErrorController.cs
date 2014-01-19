using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Controllers
{
    public class ErrorController : Controller
    {

        // GET: /Error/HttpError404
        public ActionResult HttpError404(string message)
        {
            return PartialView("HttpError404", message);
        }

        // GET: /Error/HttpError500
        public ActionResult HttpError500(string message)
        {
            return PartialView("HttpError500", message);
        }

        // GET: /Error/General
        public ActionResult General(string message)
        {
            return PartialView("General", message);
        }

    }
}
