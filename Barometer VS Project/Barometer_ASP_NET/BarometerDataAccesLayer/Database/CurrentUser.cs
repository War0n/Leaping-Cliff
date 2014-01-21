using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarometerDataAccesLayer.Database
{
	public class CurrentUser
	{
        public int StudentId { get; set; }
		public int Studentnummer { get; set; }
        public string Role { get; set; }
		public static CurrentUser instance;

		public static CurrentUser getInstance(){
			if (instance == null)
			{
				instance = new CurrentUser();

                // Set default role
                instance.Role = "guest";
                instance.Studentnummer = 0;
			}
			return instance;
		}
	}
}