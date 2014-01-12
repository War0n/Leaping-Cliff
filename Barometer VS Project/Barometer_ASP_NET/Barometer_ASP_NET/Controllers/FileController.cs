using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Controllers
{
    public class FileController : Controller
    {
        // 
        // GET: /File/

        public ActionResult StudentExport(FormCollection collection)
        {
            return RedirectToAction("Index","Home");
        }

        public ActionResult GradeExport(FormCollection collection)
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult TemplateExport(int id)
        {
            return RedirectToAction("Index", "Home");
        }

    }
}
