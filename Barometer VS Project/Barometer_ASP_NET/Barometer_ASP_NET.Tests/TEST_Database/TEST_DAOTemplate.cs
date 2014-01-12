using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarometerDataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer.Database;

namespace Barometer_ASP_NET.Tests.TEST_Database
{
    [TestClass]
    public class TEST_DAOTemplate
    {
        DAOTemplate d = new DAOTemplate();

        [TestMethod]
        public void test_01_getTemplate_valid_data()
        {
            IQueryable<BaroTemplate> b = null;
            String exception = "";
            try
            {
                b = d.getTemplate(1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.AreEqual("", exception);
            Assert.IsTrue(b.ToList().Count > 0);
        }

        [TestMethod]
        public void test_02_getTemplate_invalid_data()
        {
            IQueryable<BaroTemplate> b = null;
            String exception = "";
            try
            {
                b = d.getTemplate(-1);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("ArgumentOutOfRangeException"));
        }

        [TestMethod]
        public void test_03_getTemplate_valid_non_existent_data()
        {
            IQueryable<BaroTemplate> b = null;
            String exception = "";
            try
            {
                b = d.getTemplate(999999);
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            Assert.IsTrue(exception.Contains("DataException"));
        }
    }
}
