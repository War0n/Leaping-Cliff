using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BarometerDataAccesLayer;
using Barometer_ASP_NET.Database;
using System.Collections.Generic;

namespace Barometer_ASP_NET.Tests.TEST_Database
{
    [TestClass]
    public class TEST_DAOStudent
    {
        DAOStudent d = new DAOStudent();

        [TestMethod]
        public void test_01_getStudentGrades_invalid_input()
        {
            String exception = "";
            try
            {
                IQueryable<Report> b = d.getStudentGrades(-1, -1);
            }
            catch(Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));

            exception = "";
            try
            {
                IQueryable<Report> b = d.getStudentGrades(3000000, -1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));

            exception = "";
            try
            {
                IQueryable<Report> b = d.getStudentGrades(-1, 1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));
        }

        [TestMethod]
        public void test_02_getStudentGrades_valid_input()
        {
            IQueryable<Report> b = null;
            String exception = "";
            try
            {
                b = d.getStudentGrades(3000000, 1);
            }
            catch(Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.ToList().Count > 0);

        }

        [TestMethod]
        public void test_03_getStudentGrades_valid_nonexistent_input()
        {
            IQueryable<Report> b = null;
            String exception = "";
            try
            {
                b = d.getStudentGrades(3000000, 999999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));

        }

        [TestMethod]
        public void test_11_getStudentGroup_invalid_input()
        {
            String exception = "";
            try
            {
                IQueryable<ProjectGroup> b = d.getStudentGroup(-1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));
        }

        [TestMethod]
        public void test_12_getStudentGroup_valid_input()
        {
            IQueryable<ProjectGroup> b = null;
            String exception = "";
            try
            {
                b = d.getStudentGroup(3000000);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.ToList().Count > 0);

        }

        [TestMethod]
        public void test_13_getStudentGroup_valid_nonexistent_input()
        {
            IQueryable<ProjectGroup> b = null;
            String exception = "";
            try
            {
                b = d.getStudentGroup(1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));

        }

        [TestMethod]
        public void test_21_getReportResults_invalid_input()
        {
            String exception = "";
            try
            {
                IQueryable<ProjectGroup> b = d.getReportResults(-1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));
        }

        [TestMethod]
        public void test_22_getGetReportResults_valid_input()
        {
            IQueryable<ProjectGroup> b = null;
            String exception = "";
            try
            {
                b = d.getReportResults(3);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.ToList().Count > 0);

        }

        [TestMethod]
        public void test_23_getReportResults_valid_nonexistent_input()
        {
            IQueryable<ProjectGroup> b = null;
            String exception = "";
            try
            {
                b = d.getReportResults(999999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));

        }

        [TestMethod]
        public void test_31_getEndGradeIndividual_invalid_input()
        {
            String exception = "";
            try
            {
                int i = d.getEndGradeIndividual(-1, -1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));

            exception = "";
            try
            {
                int i = d.getEndGradeIndividual(3000000, -1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));

            exception = "";
            try
            {
                int i = d.getEndGradeIndividual(-1, 1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));
        }

        [TestMethod]
        public void test_32_getEndGradeIndividual_valid_input()
        {
            int i = 0;
            String exception = "";
            try
            {
                i = d.getEndGradeIndividual(3000000, 1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(i > 0);

        }

        [TestMethod]
        public void test_33_getEndGradeIndividual_valid_nonexistent_input()
        {
            int i = 0;
            String exception = "";
            try
            {
                i = d.getEndGradeIndividual(3000000, 999999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));

        }

        [TestMethod]
        public void test_34_getEndGradeIndividual_unclosed_project()
        {
            int i = 0;
            String exception = "";
            try
            {
                i = d.getEndGradeIndividual(3000000, 2);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.AreEqual(0, i);
        }

        [TestMethod]
        public void test_41_getEndGradeGroup_invalid_input()
        {
            String exception = "";
            try
            {
                int i = d.getEndGradeGroup(-1, -1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));

            exception = "";
            try
            {
                int i = d.getEndGradeGroup(1, -1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));

            exception = "";
            try
            {
                int i = d.getEndGradeGroup(-1, 1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));
        }

        [TestMethod]
        public void test_42_getEndGradeGroup_valid_input()
        {
            int i = 0;
            String exception = "";
            try
            {
                i = d.getEndGradeGroup(1, 1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(i > 0);

        }

        [TestMethod]
        public void test_43_getEndGradeGroup_valid_nonexistent_input()
        {
            int i = 0;
            String exception = "";
            try
            {
                i = d.getEndGradeGroup(3000000, 999999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));

        }

        [TestMethod]
        public void test_44_getEndGradeGroup_unclosed_project()
        {
            int i = 0;
            String exception = "";
            try
            {
                i = d.getEndGradeGroup(1, 2);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));
        }

        [TestMethod]
        public void test_51_getReportResults_invalid_input()
        {
            String exception = "";
            try
            {
                Dictionary<string, string> b = d.getName(-1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRange"));
        }

        [TestMethod]
        public void test_52_getGetReportResults_valid_input()
        {
            Dictionary<string, string> b = null;
            String exception = "";
            try
            {
                b = d.getName(3000000);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.ToList().Count > 0);

        }

        [TestMethod]
        public void test_53_getReportResults_valid_nonexistent_input()
        {
            Dictionary<string, string> b = null;
            String exception = "";
            try
            {
                b = d.getName(43);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));

        }


    }
}
