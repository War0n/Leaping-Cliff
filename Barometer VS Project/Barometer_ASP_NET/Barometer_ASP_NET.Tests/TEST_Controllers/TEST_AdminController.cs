using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Barometer_ASP_NET.Controllers;
using BarometerDataAccesLayer;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Web.Routing;
using System.IO;
using System.Configuration;
using BarometerDataAccesLayer.Database;

namespace Barometer_ASP_NET.Tests.TEST_Controllers
{
    [TestClass]
    public class TEST_AdminController
    {
        AdminController a = new AdminController();
        private FileStream _stream;

        [TestInitialize]
        public void SetUp()
        {
            _stream = new FileStream("../../../Barometer_ASP_NET/Content/ExcelTemplates/GradeTemplate.xlsx",
                         FileMode.Open);

        }

        [TestMethod]
        public void test_01_student_correct_testdata()
        {
            var result = a.Student(1) as ViewResult;
            var resultdata = (User)result.ViewData.Model;

            Assert.AreEqual("John ", resultdata.firstname);
        }

        [TestMethod]
        public void test_02_studentForm()
        {
            var controller = new AdminController();
            FormCollection a = new FormCollection();
            a.Add("student", "3000000");
            var result = (RedirectToRouteResult) controller.StudentForm(a);
            Assert.AreEqual("Student", result.RouteValues["action"]);
        }

        //[TestMethod]
        public void test_03_create_and_delete_project() 
        {
            var controller = new AdminController();

            var server = new Mock<HttpServerUtilityBase>();
            var response = new Mock<HttpResponseBase>();

            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r.UserHostAddress).Returns("localhost");

            var session = new Mock<HttpSessionStateBase>();
            session.Setup(s => s.SessionID).Returns(Guid.NewGuid().ToString());

            var file = new Mock<HttpPostedFileBase>();
            file.Setup(x => x.InputStream).Returns(_stream);
            file.Setup(x => x.ContentLength).Returns((int)_stream.Length);
            file.Setup(x => x.FileName).Returns(_stream.Name);

            var files = new Mock<HttpFileCollectionBase>();
            files.Setup(r => r.Count).Returns(1);
            files.Setup(r => r.Get(0).InputStream).Returns(file.Object.InputStream);
            request.Setup(r => r.Files).Returns(files.Object);
            //request.Setup(r => r.Files[0]).Returns(file.Object);

            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Request).Returns(request.Object);
            context.SetupGet(c => c.Request).Returns(request.Object);
            context.SetupGet(c => c.Response).Returns(response.Object);
            context.SetupGet(c => c.Server).Returns(server.Object);
            context.SetupGet(c => c.Session).Returns(session.Object);


            controller.ControllerContext = new ControllerContext(context.Object,
                                              new RouteData(), controller);

            CurrentUser currentUser = CurrentUser.getInstance();
            currentUser.Studentnummer = 3000000;

            FormCollection n = new FormCollection();
            string exception = "";
            n.Add("FormProject.name", "TestProject");
            n.Add("FormProject.description", "TestDescription");
            n.Add("FormProject.start_date", "01/01/2001");
            n.Add("FormProject.end_date", "12/12/2012");
            try
            {
                var result = (RedirectToRouteResult) controller.Create(n);
                Assert.AreEqual("List", result.RouteValues["action"]);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual(exception, "");

            exception = "";

            try{
                a.DeleteProject(8);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.AreEqual(exception, "");
        }
    }
}
