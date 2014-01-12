using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarometerDataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Barometer_ASP_NET.Database;
using Barometer_ASP_NET.FileFactory;
using Barometer_ASP_NET.Models;

namespace Barometer_ASP_NET.Tests.TEST_FileFactory
{
    [TestClass]
    public class TEST_FileHandlerFactory
    {
        [TestMethod]
        public void test_01_create_excel_writer()
        {
            //wtf is dit uberhaupt?
            TemplateExcel b = null;
            String exception = "";
            try
            {
                b = FileHandlerFactory<TemplateExcel>.create("Template");
            }catch(Exception e){
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b != null);
        }
    }
}
