using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Barometer_ASP_NET.Controllers;
using BarometerDataAccesLayer;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Tests.TEST_Controllers
{
    [TestClass]
    public class TEST_AdminController
    {
        AdminController a = new AdminController();

        [TestMethod]
        public void test_01_student_correct_testdata()
        {
            var result = a.Student(3000000) as ViewResult;
            var resultdata = (User) result.ViewData.Model;

            Assert.AreEqual("John", resultdata.firstname);
        }
    }
}
