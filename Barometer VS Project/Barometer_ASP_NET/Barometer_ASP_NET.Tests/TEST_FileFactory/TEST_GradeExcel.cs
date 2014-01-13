using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Barometer_ASP_NET.FileFactory;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Barometer_ASP_NET.Tests.TEST_FileFactory
{
    [TestClass]
    public class TEST_GradeExcel
    {
        [TestMethod]
        public void test_01_export_grade_excel()
        {
            GradeExcel g = new GradeExcel();
            g.Export(3000000);
            String exception = "";
            try
            {
                Assert.IsTrue(File.Exists("~/Content/ExcelExports/3000000.xlsx"));
                File.Delete("~/Content/ExcelExports/3000000.xlsx");
            }
            catch(Exception e)
            {
                exception = e.ToString();
            }

            Assert.AreEqual("", exception);
        }
    }
}
