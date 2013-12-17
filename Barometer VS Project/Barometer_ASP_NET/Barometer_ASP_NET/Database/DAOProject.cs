using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Barometer_ASP_NET.Database
{
	public class DAOProject
	{
		/// <summary>
		/// Example method
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
        public string getChallenges(int id) {
            string ProjectName = null;
            try
            {
                LinqToSqlDataContext db = new LinqToSqlDataContext();

                var projects = from p in db.projects
                               where p.name.Contains("SO")
                               select p;
            }
            catch (Exception e) 
            {
                Console.WriteLine("Error in getChallenges: " + e);
            }
            return ProjectName;
        }
	}
}
