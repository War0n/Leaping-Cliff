using Barometer_ASP_NET.FileFactory;
using Barometer_ASP_NET.Wrappers;
using BarometerDataAccesLayer.Database;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Controllers
{
    public class AdminController : Controller
    {
        AdminDashboardWrapper wrapper = new AdminDashboardWrapper(3242344);
        //
        // GET: /Admin/

        public ActionResult Dashboard()
        {
            return View(wrapper);
        }

		public ActionResult Barotemplate()
		{
			return View();
		}

		public ActionResult Create()
		{
            CreateProjectWrapper wrapper = new CreateProjectWrapper();
			return View(wrapper);
		}

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (collection.Count != 0)
            {
                BarometerDataAccesLayer.DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
                BarometerDataAccesLayer.Project insertProject = new BarometerDataAccesLayer.Project();
                insertProject.name = collection.GetValue("FormProject.name").AttemptedValue;
                insertProject.description = collection.GetValue("FormProject.description").AttemptedValue;

                insertProject.start_date = DateTime.ParseExact(collection.GetValue("FormProject.start_date").AttemptedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                insertProject.end_date = DateTime.ParseExact(collection.GetValue("FormProject.end_date").AttemptedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (Request.Files.Count != 0)
                {
                    HttpPostedFileBase fileBase = Request.Files[0];
                    StudentExcel studentExcel = new StudentExcel();
                    studentExcel.Import(fileBase.InputStream);
                    context.Projects.InsertOnSubmit(insertProject);
                    studentExcel.AddGroupsToProject(fileBase.InputStream, insertProject);


                    insertProject.baro_template_id = int.Parse(collection.GetValue("FormProject.baro_template_id").AttemptedValue);

                    string[] reportDateNames = collection.GetValue("reportDateName[]").AttemptedValue.Split(',');
                    string[] reportStartDates = collection.GetValue("reportStartDate[]").AttemptedValue.Split(',');
                    string[] reportEndDates = collection.GetValue("reportEndDate[]").AttemptedValue.Split(',');
                    EntitySet<BarometerDataAccesLayer.ProjectReportDate> reportDateEntities = new EntitySet<BarometerDataAccesLayer.ProjectReportDate>();

                    int counter = 0;
                    foreach (string reportDateName in reportDateNames)
                    {
                        BarometerDataAccesLayer.ProjectReportDate tmpReportDate = new BarometerDataAccesLayer.ProjectReportDate();
                        tmpReportDate.Project = insertProject;
                        tmpReportDate.week_label = reportDateName;
                        tmpReportDate.start_date = DateTime.ParseExact(reportStartDates[counter], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        tmpReportDate.end_date = DateTime.ParseExact(reportStartDates[counter], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        context.ProjectReportDates.InsertOnSubmit(tmpReportDate);
                        counter++;
                    }
                    context.SubmitChanges();
                }
            }
            return RedirectToAction("List");
        }

		public ActionResult Detail()
		{
			return View();
		}

		public ActionResult List()
		{
			return View(wrapper);
		}

		public ActionResult ProjectGroups()
		{
			return View();
		}

		public ActionResult ProjectGroupsView()
		{
			return View();
		}

        public ActionResult Student(int studentId)
        {
            BarometerDataAccesLayer.DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            var student =
                from u in context.Users
                where u.id == studentId
                select u;
            return View(student.First());
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
            return RedirectToAction("Student",new {studentId = studentId});
        }

        public ActionResult DeleteProject(int projectId)
        {
            BarometerDataAccesLayer.DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            var resultProjects=
                from p in context.Projects
                where p.id == projectId
                select p;

            context.Projects.DeleteOnSubmit(resultProjects.First());
            context.SubmitChanges();

            return RedirectToAction("List");
        }
    }
}
