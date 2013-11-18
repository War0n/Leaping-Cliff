using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.Models
{
	public class Grade
	{
		public string Criterium { get; set; }
		public double Grade { get; set; }
		public ProjectGroup AttachedProject { get; set; }

		public Grade(string crit, double grade, ProjectGroup project)
		{
			Criterium = crit;
			Grade = grade;
			AttachedProject = project;
		}
	}
}