using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Barometer_ASP_NET.FileFactory;
using Moq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Tests.TEST_FileFactory
{
    [TestClass]
    public class TEST_StudentExcel
    {
        String path = "";

        //[TestMethod]
        //public void test_01_export_studentexcel()
        //{
        //    var controller = new StudentExcel();

        //    var server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
        //    var response = new Mock<HttpResponseBase>(MockBehavior.Strict);

        //    var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
        //    request.Setup(r => r.UserHostAddress).Returns("localhost");

        //    var session = new Mock<HttpSessionStateBase>();
        //    session.Setup(s => s.SessionID).Returns(Guid.NewGuid().ToString());

        //    var context = new Mock<HttpContextBase>();
        //    context.SetupGet(c => c.Request).Returns(request.Object);
        //    context.SetupGet(c => c.Response).Returns(response.Object);
        //    context.SetupGet(c => c.Server).Returns(server.Object);
        //    context.SetupGet(c => c.Session).Returns(session.Object);

        //    controller.ControllerContext = new ControllerContext(context.Object,
        //                                      new RouteData(), controller);

        //    int[] students = new int[1] { 3000000 };
        //    try
        //    {
        //        s.Export(students);
        //        string path = "~/Content/ExcelExports/" + "Students" + DateTime.Now.ToShortDateString() + ".xlsx";
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        public void test_02_import_studentexcel()
        {
            //todo when export works
            //System.IO.FileStream = new FileStream(path, null);
        }
    }
}
