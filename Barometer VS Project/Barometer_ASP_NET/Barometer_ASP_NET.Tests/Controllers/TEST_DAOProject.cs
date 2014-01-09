using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Barometer_ASP_NET.Database;
using System.Linq;
using BarometerDataAccesLayer;
using System.Collections.Generic;

namespace Barometer_ASP_NET.Tests.Controllers
{
    [TestClass]
    public class TEST_DAOProject
    {
        DAOProject d = new DAOProject();

        [TestMethod]
        public void test_01_getstudents_negative_input()
        {
            String s = "";
            try
            {
                IQueryable<User> test_collection = d.getStudents(-1);
            }
            catch(Exception e)
            {
                s = e.ToString();
            }

            Assert.IsTrue(s.Contains("ArgumentOutOfRangeException"));

        }

        [TestMethod]
        public void test_02_getstudents_valid_input()
        {
            IQueryable<User> test_collection;
            int i = 0;
            
            try
            {
                test_collection = d.getStudents(1);
                foreach (User u in test_collection)
                {
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Assert.IsTrue(i > 0);
        }

        [TestMethod]
        public void test_03_getStudents_valid_nonexistent_input()
        {
            IQueryable<User> test_collection;
            String exception = "";
            try
            {
                test_collection = d.getStudents(99924);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("DataException"));
        }

        [TestMethod]
        public void test_11_getprojectgroupmembers_negative_input()
        {
            String s = "";
            try
            {
                IQueryable<ProjectMember> test_collection = d.getProjectGroupMembers(-1);
            }
            catch (Exception e)
            {
                s = e.ToString();
            }

            Assert.IsTrue(s.Contains("ArgumentOutOfRangeException")); 

        }

        [TestMethod]
        public void test_12_getprojectgroupmembers_valid_input()
        {
            String exception = "";
            IQueryable<ProjectMember> test_collection;
            int i = 0;
            try
            {
                test_collection = d.getProjectGroupMembers(3000000);
                foreach (ProjectMember u in test_collection)
                {
                    i++;
                }
            }
            catch (Exception e)
            {
               exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(i > 0);
        }

        [TestMethod]
        public void test_13_getProjectGroupMembers_valid_nonexistent_input()
        {
            String exception = "";
            IQueryable<ProjectMember> test_collection;
            try
            {
                test_collection = d.getProjectGroupMembers(3000000);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));
        }

        [TestMethod]
        public void test_21_gettutors_negative_input()
        {
            String s = "";
            try
            {
                Dictionary<string, string> b = d.getTutors(-1);
            }
            catch (Exception e)
            {
                s = e.ToString();
            }

            Assert.IsTrue(s.Contains("ArgumentOutOfRangeException"));

        }

        [TestMethod]
        public void test_22_getTutors_valid_input()
        {
            String exception = "";
            Dictionary<string, string> b = new Dictionary<string, string>();
            try
            {
                b = d.getTutors(1);
            }
            catch(Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.Count > 0);

            exception = "";
            
            try
            {
                b = d.getTutors(99999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.Count == 0);


        }

        [TestMethod]
        public void test_31_getNamesOfGroupMembers_invalid_input()
        {
            String exception = "";
            try
            {
                Dictionary<string, string> b = d.getNamesOfGroupMembers(-1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("ArgumentOutOfRangeException"));
        }

        [TestMethod]
        public void test_32_getNamesOfGroupMembers_valid_input()
        {
            String exception = "";
            Dictionary<string, string> b = new Dictionary<string, string>();
            try
            {
                b = d.getNamesOfGroupMembers(1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.Count > 0);

            exception = "";

            try
            {
                b = d.getNamesOfGroupMembers(99999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.Count == 0);
        }

        [TestMethod]
        public void test_41_getProjectName_invalid_input()
        {
            String exception = "";
            try
            {
               String pName = d.getProjectName(-1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("ArgumentOutOfRangeException"));
        }

        [TestMethod]
        public void test_42_getProjectName_valid_input()
        {

        }
    }
}
