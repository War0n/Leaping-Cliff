using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Barometer_ASP_NET.FileFactory;

namespace Barometer_ASP_NET.Tests.TEST_FileFactory
{
    [TestClass]
    public class TEST_StudentExcel
    {
        String path = "";

        //obsolete
        //[TestMethod]
        //public void test_01_export_studentexcel()
        //{
        //    StudentExcel s = new StudentExcel();
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
