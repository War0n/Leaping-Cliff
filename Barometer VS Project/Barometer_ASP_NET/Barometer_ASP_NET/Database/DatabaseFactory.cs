using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
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
		public SqlConnection makeConnection()
		{
			String connectionString = "Server=mssql.aii.avans.nl;Database=LeapingCliff;Trusted_Connection=False;";
            Char[] pwc = "Ab12345".ToCharArray();
            SecureString pwd = new SecureString();
            foreach (char ch in pwc)
            {
                pwd.AppendChar(ch);
            }
            pwd.MakeReadOnly();
            pwc = null;
            SqlCredential _credential = new SqlCredential("42IN05SOb", pwd);
            SqlConnection connection = new SqlConnection(connectionString, _credential);
            return connection;
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