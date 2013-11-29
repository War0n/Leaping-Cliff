using System;
using System.Collections.Generic;
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
            try {
               // Connection connection = DatabaseFactory.getInstance().makeConnection();

              //  String sql = "SELECT * FROM project WHERE projectid = " + id + ";";

               // Statement selectStatement = connection.createStatement();
               // ResultSet results = selectStatement.executeQuery(sql);

				//ProjectName = results.getString();

               // connection.close();
               // results.close();
               // selectStatement.close();

            } catch (Exception e) {
                Console.WriteLine("Error in getChallenges: " + e);
            }
            return ProjectName;
        }
	}
}
