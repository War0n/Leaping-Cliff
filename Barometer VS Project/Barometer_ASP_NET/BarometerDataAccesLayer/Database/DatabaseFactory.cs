using BarometerDataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Web;

namespace BarometerDataAccesLayer.Database
{
	public class DatabaseFactory
	{
		private DAOProject DAOPROJECT = new DAOProject();
		private static DatabaseFactory instance = null;
        private DAOStudent DAOSTUDENT = new DAOStudent();
        private static DatabaseClassesDataContext DataContext { get; set; }
        private DAOTemplate DAOTEMPLATE = new DAOTemplate();

		/// <summary>
		/// Singleton pattern to get the DatabaseFactory Object
		/// </summary>
		/// <returns>An instance of the DatabaseFactory</returns>
		public static DatabaseFactory getInstance()
		{
			if (instance == null)
			{
				instance = new DatabaseFactory();
			}
			return instance;
		}

        /// <summary>
        /// Singleton pattern to get the DataContext Object
        /// </summary>
        /// <returns>DatabaseClassesDataContext</returns>
        public DatabaseClassesDataContext getDataContext()
        {
            if (DataContext == null)
            {
                DataContext = new DatabaseClassesDataContext();
            }
            return DataContext;
        }

		/// <summary>
		/// Method to get the object of the Database-Model class back
		/// </summary>
		/// <returns></returns>
		public DAOProject getDAOProject()
		{
			return DAOPROJECT;
		}

        public DAOStudent getDAOStudent()
        {
            return DAOSTUDENT;
        }

        public DAOTemplate getDAOTemplate()
        {
            return DAOTEMPLATE;
        }
    }
}