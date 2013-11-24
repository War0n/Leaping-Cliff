using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.Database
{
	public class DatabaseFactory
	{
		private DAOProject DAOPROJECT = new DAOProject();
		private static DatabaseFactory instance = null;

		/// <summary>
		/// Singleton pattern to get the DatabaseFactory Object
		/// </summary>
		/// <returns></returns>
		public static DatabaseFactory getInstance()
		{
			if (instance == null)
			{
				instance = new DatabaseFactory();
			}
			return instance;
		}

		/// <summary>
		/// Method to make the connection with the database
		/// </summary>
		/// <returns></returns>
		public Connection makeConnection()
		{
			//hier komt de connectie met het db
		}

		/// <summary>
		/// Method to get the object of the Database-Model class back
		/// </summary>
		/// <returns></returns>
		public DAOProject getDAOPerson()
		{
			return DAOPROJECT;
		}
	}
}