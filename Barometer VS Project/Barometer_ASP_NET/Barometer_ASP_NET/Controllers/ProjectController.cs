using Barometer_ASP_NET.Database;
using Barometer_ASP_NET.Models;
using BarometerDataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/
        public DAOStudent student = new DAOStudent();
        public DAOProject project = new DAOProject();

        public ActionResult Index()
        {
            return View();
        }

        //Detail Function
        public string ShowNameOfStudent()
        {
            Dictionary<string, string> d = student.getName(3000000);//IMPORTANT, uses a test value, it needs the studentnumber when LDAP is finished.
            string s ="";
            foreach (KeyValuePair<string, string> pair in d)
            {
                 s = pair.Key + " " + pair.Value;
            }

            return s;
        }

        public string ShowNameOfProject()
        {
            string s = project.getProjectName(1);//IMPORTANT, uses a test value.
            return s;
        }

        public string ShowSummaryOfProject()
        {
            string s = project.getProjectSummary(1);
            return s;
        }

        public string ShowStartAndEndDate()
        {
            Dictionary<string, string> d = project.getStartAndEndDate(1);//IMPORTANT, uses a test value
            string s = "";
            foreach (KeyValuePair<string, string> pair in d)
            {
                s = pair.Key + " tot " + pair.Value + ")";
            }

            return s;
        }

        public ViewResult Project()
        {
            return View();
        }

    }
}
