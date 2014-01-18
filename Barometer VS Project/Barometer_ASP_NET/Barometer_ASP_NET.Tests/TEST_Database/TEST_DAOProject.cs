using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarometerDataAccesLayer.Database;
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
                test_collection = d.getProjectGroupMembers(1);
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
        }

        [TestMethod]
        public void test_23_getTutors_valid_nonexistent_input()
        {
            String exception = "";
            Dictionary<string, string> b = new Dictionary<string, string>();

            try
            {
                b = d.getTutors(99999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));
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
        }

        [TestMethod]
        public void test_33_getNamesOfGroupMembers_valid_nonexistent_input()
        {
            String exception = "";
            Dictionary<string, string> b = new Dictionary<string, string>();
            try
            {
                b = d.getNamesOfGroupMembers(99999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));
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
            String exception = "";
            String pName = "";
            try
            {
                pName = d.getProjectName(1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.AreEqual("", exception);
            Assert.AreNotEqual("", pName);
        }

        [TestMethod]
        public void test_43_getProjectName_valid_nonexistent_input()
        {
            String exception = "";
            String pName = "";

            try
            {
                pName = d.getProjectName(9999999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("DataException"));
        }

        [TestMethod]
        public void test_51_getStartAndEndDate_invalid_input()
        {
            String exception = "";
            try
            {
                Dictionary<string, string> b = d.getStartAndEndDate(-1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("ArgumentOutOfRangeException"));
        }

        [TestMethod]
        public void test_52_getStartAndEndDate_valid_input()
        {
            String exception = "";
            Dictionary<string, string> b = new Dictionary<string, string>();
            try
            {
                b = d.getStartAndEndDate(1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.Count > 0);
        }

        [TestMethod]
        public void test_53_getStartAndEndDate_valid_nonexistent_input()
        {
            String exception = "";
            Dictionary<string, string> b = new Dictionary<string, string>();
            try
            {
                b = d.getStartAndEndDate(99999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));
        }

        [TestMethod]
        public void test_61_getProjectSummary_invalid_input()
        {
            String exception = "";
            try
            {
                String pSummary = d.getProjectSummary(-1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("ArgumentOutOfRangeException"));
        }

        [TestMethod]
        public void test_62_getProjectSummary_valid_input()
        {
            String exception = "";
            String pSummary = "";
            try
            {
                pSummary = d.getProjectSummary(1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.AreEqual("", exception);
            Assert.AreNotEqual("", pSummary);
        }

        [TestMethod]
        public void test_63_getProjectSummary_valid_nonexistent_input()
        {
            String exception = "";
            String pSummary = "";

            try
            {
                pSummary = d.getProjectSummary(9999999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("DataException"));
        }

        [TestMethod]
        public void test_71_getProjectTeachers_invalid_input()
        {
            String exception = "";
            try
            {
                Dictionary<string, string> b = d.getProjectTeachers(-1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("ArgumentOutOfRangeException"));
        }

        [TestMethod]
        public void test_72_getProjectTeachers_valid_input()
        {
            String exception = "";
            Dictionary<string, string> b = new Dictionary<string, string>();
            try
            {
                b = d.getProjectTeachers(1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.Count > 0);
        }

        [TestMethod]
        public void test_73_getProjectTeachers_valid_nonexistent_input()
        {
            String exception = "";
            Dictionary<string, string> b = new Dictionary<string, string>();
            try
            {
                b = d.getProjectTeachers(99999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));
        }

        [TestMethod]
        public void test_81_getUsersInGroup_negative_input()
        {
            String s = "";
            try
            {
                IQueryable<User> test_collection = d.getUsersInGroup(-1);
            }
            catch (Exception e)
            {
                s = e.ToString();
            }

            Assert.IsTrue(s.Contains("ArgumentOutOfRangeException"));

        }

        [TestMethod]
        public void test_82_getUsersInGroup_valid_input()
        {
            IQueryable<User> test_collection;
            int i = 0;
            String exception = "";

            try
            {
                test_collection = d.getUsersInGroup(1);
                foreach (User u in test_collection)
                {
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Assert.AreEqual("", exception);
            Assert.IsTrue(i > 0);
        }

        [TestMethod]
        public void test_83_getUsersInGroup_valid_nonexistent_input()
        {
            IQueryable<User> test_collection;
            String exception = "";
            try
            {
                test_collection = d.getUsersInGroup(99924);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("DataException"));
        }

        [TestMethod]
        public void test_91_getProjectOwners_negative_input()
        {
            String s = "";
            try
            {
                IQueryable<User> test_collection = d.GetProjectOwners(-1);
            }
            catch (Exception e)
            {
                s = e.ToString();
            }

            Assert.IsTrue(s.Contains("ArgumentOutOfRangeException"));

        }

        [TestMethod]
        public void test_92_getProjectOwners_valid_input()
        {
            IQueryable<User> test_collection;
            int i = 0;
            String exception = "";

            try
            {
                test_collection = d.GetProjectOwners(1);
                foreach (User u in test_collection)
                {
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Assert.AreEqual("", exception);
            Assert.IsTrue(i > 0);
        }

        [TestMethod]
        public void test_93_getProjectOwners_valid_nonexistent_input()
        {
            IQueryable<User> test_collection;
            String exception = "";
            try
            {
                test_collection = d.GetProjectOwners(99924);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("DataException"));
        }

        [TestMethod]
        public void test_101_getTutor_negative_input()
        {
            String s = "";
            try
            {
                IQueryable<User> test_collection = d.GetTutor(-1);
            }
            catch (Exception e)
            {
                s = e.ToString();
            }

            Assert.IsTrue(s.Contains("ArgumentOutOfRangeException"));

        }

        [TestMethod]
        public void test_102_getTutor_valid_input()
        {
            IQueryable<User> test_collection;
            int i = 0;
            String exception = "";

            try
            {
                test_collection = d.GetTutor(1);
                foreach (User u in test_collection)
                {
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Assert.AreEqual("", exception);
            Assert.IsTrue(i > 0);
        }

        [TestMethod]
        public void test_103_getTutor_valid_nonexistent_input()
        {
            IQueryable<User> test_collection;
            String exception = "";
            try
            {
                test_collection = d.GetTutor(99924);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            Assert.IsTrue(exception.Contains("DataException"));
        }

        [TestMethod]
        public void test_111_getCurrentActiveProject_negative_input()
        {
            String s = "";
            try
            {
                IQueryable<Project> test_collection = d.GetCurrentActiveProject(-1);
            }
            catch (Exception e)
            {
                s = e.ToString();
            }

            Assert.IsTrue(s.Contains("ArgumentOutOfRangeException"));

        }

        [TestMethod]
        public void test_112_getCurrentActiveProject_valid_input()
        {
            IQueryable<Project> test_collection;
            int i = 0;
            String exception = "";

            try
            {
                test_collection = d.GetCurrentActiveProject(3000000);
                foreach (Project u in test_collection)
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

        // Obsolete
        //[TestMethod]
        //public void test_113_getCurrentActiveProject_valid_nonexistent_input()
        //{
        //    IQueryable<Project> test_collection;
        //    String exception = "";
        //    try
        //    {
        //        test_collection = d.GetCurrentActiveProject(9);
        //    }
        //    catch (Exception e)
        //    {
        //        exception = e.ToString();
        //    }

        //    Assert.IsTrue(exception.Contains("DataException"));
        //}
    }
}
