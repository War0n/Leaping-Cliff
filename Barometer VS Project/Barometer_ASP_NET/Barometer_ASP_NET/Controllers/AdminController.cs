using Barometer_ASP_NET.FileFactory;
using Barometer_ASP_NET.Wrappers;
using BarometerDataAccesLayer.Database;
using BarometerDataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barometer_ASP_NET.Filters;

namespace Barometer_ASP_NET.Controllers
{
    [AuthFilter("admin")]
    public class AdminController : Controller
    {
        AdminDashboardWrapper wrapper = new AdminDashboardWrapper(CurrentUser.getInstance().Studentnummer);
        //
        // GET: /Admin/

        public ActionResult Index()
        {

            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            return View(wrapper);
        }

        DatabaseClassesDataContext db = DatabaseFactory.getInstance().getDataContext();

        public ActionResult Barotemplate()
        {
            int debug_id = (Request["template_id"] == null) ? 0 : Convert.ToInt32(Request["template_id"]);
            ViewBag.debug_id = debug_id;
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
                BarometerDataAccesLayer.ProjectOwner me = new BarometerDataAccesLayer.ProjectOwner();
                var ownerInfo =
                    from u in context.Users
                    where u.student_number == CurrentUser.getInstance().Studentnummer
                    select u;
                me.User = ownerInfo.First();
                insertProject.name = collection.GetValue("FormProject.name").AttemptedValue;
                insertProject.description = collection.GetValue("FormProject.description").AttemptedValue;
                insertProject.start_date = DateTime.ParseExact(collection.GetValue("FormProject.start_date").AttemptedValue, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                insertProject.end_date = DateTime.ParseExact(collection.GetValue("FormProject.end_date").AttemptedValue, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                insertProject.ProjectOwners.Add(me);
                insertProject.status_name = "Pending";
                if (Request.Files.Count != 0)
                {
                    HttpPostedFileBase fileBase = Request.Files[0];
                    StudentExcel studentExcel = new StudentExcel(insertProject);
                    studentExcel.Import(fileBase.InputStream);


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
                        tmpReportDate.start_date = DateTime.Parse(reportStartDates[counter]);
                        tmpReportDate.end_date = DateTime.Parse(reportEndDates[counter]);
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

        public ActionResult ListProjectGroup(int projectId)
        {
            AdminProjectGroupListWrapper wrapper = new AdminProjectGroupListWrapper(projectId);
            return View(wrapper);
        }

        public ActionResult ProjectGroups(int groupId)
        {
            AdminProjectGroupViewWrapper wrapper = new AdminProjectGroupViewWrapper(groupId);
            return View(wrapper);
        }

        [AuthFilter("admin")]
        [AuthFilter("moderator")]
        public ActionResult Student(int studentId)
        {
            BarometerDataAccesLayer.DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            var student =
                from u in context.Users
                where u.id == studentId
                select u;
            if (student.ToList().Count > 0)
            {
                return View(student.First());
            }
            else
            {
                throw new DataException("No data found");
            }
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
            return RedirectToAction("Student", new { studentId = studentId });
        }

        public ActionResult DeleteProject(int projectId)
        {
            BarometerDataAccesLayer.DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            var resultProjects =
                from p in context.Projects
                where p.id == projectId
                select p;

            context.Projects.DeleteOnSubmit(resultProjects.First());
            context.SubmitChanges();

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult AddStudent(FormCollection collection)
        {
            int studentIdToAdd = int.Parse(collection.GetValue("student").AttemptedValue);
            int projectId = int.Parse(collection.GetValue("projectID").AttemptedValue);
            int groupId = int.Parse(collection.GetValue("groupID").AttemptedValue);
            DAOStudent studentdao = DatabaseFactory.getInstance().getDAOStudent();
            DAOProject projectdao = DatabaseFactory.getInstance().getDAOProject();
            BarometerDataAccesLayer.User studentUser = studentdao.getStudentInfo(studentIdToAdd);
            IEnumerable<BarometerDataAccesLayer.ProjectMember> member = studentUser.ProjectMembers.Where(pm => pm.project_group_id == groupId);
            if (member.Count() == 0)
            {
                BarometerDataAccesLayer.ProjectMember pMember = new BarometerDataAccesLayer.ProjectMember();
                pMember.User = studentUser;
                pMember.project_group_id = groupId;
                BarometerDataAccesLayer.DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
                context.ProjectMembers.InsertOnSubmit(pMember);
                context.SubmitChanges();
            }

            return RedirectToAction("ProjectGroups", new { groupId = groupId });
        }
    }
}
