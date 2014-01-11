using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Barometer_ASP_NET.Database;
using System.Linq;
using BarometerDataAccesLayer;

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
                s = e.Message;
            }

            Assert.AreEqual("range", s);

        }

        [TestMethod]
        public void test_02_getstudents_valid_input()
        {
            IQueryable<User> test_collection;
            int i = 0;
            try
            {
                test_collection = d.getStudents(99924);
                foreach (User u in test_collection)
                {
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Assert.IsTrue(i == 0);
            
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
        public void test_11_getprojectgroupmembers_negative_input()
        {
            String s = "";
            try
            {
                IQueryable<ProjectMember> test_collection = d.getProjectGroupMembers(-1);
            }
            catch (Exception e)
            {
                s = e.Message;
            }

            Assert.AreEqual("range", s);

        }

        [TestMethod]
        public void test_12_getprojectgroupmembers_valid_input()
        {
            IQueryable<ProjectMember> test_collection;
            int i = 0;
            try
            {
                test_collection = d.getProjectGroupMembers(99924);
                foreach (ProjectMember u in test_collection)
                {
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Assert.IsTrue(i == 0);

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
                Console.WriteLine(e.Message);
            }

            Assert.IsTrue(i > 0);
        }

        public void test_21_getprojectgroupmembers_negative_input()
        {
            String s = "";
            try
            {
                IQueryable<ProjectMember> test_collection = d.getProjectGroupMembers(-1);
            }
            catch (Exception e)
            {
                s = e.Message;
            }

            Assert.AreEqual("range", s);

        }

        [TestMethod]
        public void test_22_getprojectgroupmembers_valid_input()
        {
            IQueryable<ProjectMember> test_collection;
            int i = 0;
            try
            {
                test_collection = d.getProjectGroupMembers(99924);
                foreach (ProjectMember u in test_collection)
                {
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Assert.IsTrue(i == 0);

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
                Console.WriteLine(e.Message);
            }

            Assert.IsTrue(i > 0);
        }


    }
}
