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
using System.Security.Principal;

namespace Barometer_ASP_NET.Tests.TEST_Controllers
{
    [TestClass]
    public class TEST_AdminController
    {
        AdminController a = new AdminController();
        private FileStream _stream;

        private HttpContextBase GetMockedHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();
            var urlHelper = new Mock<UrlHelper>();
            var file = new Mock<HttpPostedFileBase>();

            file.Setup(x => x.InputStream).Returns(_stream);
            file.Setup(x => x.ContentLength).Returns((int)_stream.Length);
            file.Setup(x => x.FileName).Returns(_stream.Name);

            var files = new Mock<HttpFileCollectionBase>();
            files.Setup(r => r.Count).Returns(1);
            files.Setup(r => r.Get(0).InputStream).Returns(file.Object.InputStream);

            var requestContext = new Mock<RequestContext>();
            requestContext.Setup(x => x.HttpContext).Returns(context.Object);
            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);
            context.Setup(ctx => ctx.User).Returns(user.Object);
            user.Setup(ctx => ctx.Identity).Returns(identity.Object);
            identity.Setup(id => id.IsAuthenticated).Returns(true);
            identity.Setup(id => id.Name).Returns("test");
            request.Setup(req => req.Url).Returns(new Uri("http://www.google.com"));
            request.Setup(req => req.RequestContext).Returns(requestContext.Object);
            requestContext.Setup(x => x.RouteData).Returns(new RouteData());
            request.Setup(r => r.Files).Returns(files.Object);

            return context.Object;
        }

        //[TestInitialize]
        public void SetUp()
        {
            _stream = new FileStream("../../../Barometer_ASP_NET/Content/TestTemplate/TestTemplate.xlsx",
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

            HttpContextFactory.SetCurrentContext(GetMockedHttpContext());
            controller.ControllerContext = new ControllerContext(HttpContextFactory.Current,
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
