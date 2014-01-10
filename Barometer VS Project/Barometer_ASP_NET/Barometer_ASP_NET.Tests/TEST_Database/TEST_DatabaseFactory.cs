using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarometerDataAccesLayer;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Web;
using Barometer_ASP_NET.Database;

namespace Barometer_ASP_NET.Tests.TEST_Database
{
    [TestClass]
    public class TEST_DatabaseFactory
    {
        [TestMethod]
        public void test_01_is_singleton()
        {
            DatabaseFactory a = DatabaseFactory.getInstance();
            DatabaseFactory b = DatabaseFactory.getInstance();
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void test_02_datacontext_is_singleton()
        {
            DatabaseFactory d = DatabaseFactory.getInstance();
            DatabaseClassesDataContext a = d.getDataContext();
            DatabaseClassesDataContext b = d.getDataContext();
            Assert.AreEqual(a, b);
        }
    }
}
