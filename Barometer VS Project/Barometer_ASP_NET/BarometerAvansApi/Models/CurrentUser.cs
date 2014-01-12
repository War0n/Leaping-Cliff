using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarometerAvansApi.Models
{
	public class CurrentUser
	{
		public int Studentnummer { get; set; }
		public static CurrentUser instance;

		public static CurrentUser getInstance(){
			if (instance == null)
			{
				instance = new CurrentUser();
			}
			return instance;
		}
	}
}