using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.Models
{
	public class Student
	{
		public string Name { get; set; }
		public string Class { get; set; }
		public int StudentNumber { get; set; }
		public List<Grade> Grades { get; set; }

		public Student(string name, string clas, int studnr)
		{
			Name = name;
			Class = clas;
			StudentNumber = studnr;
			Grades = new List<Grade>();
		}

		public void AddGrade(string crit, double grade, ProjectGroup projectGroup)
		{
			Grades.Add(new Grade(crit, grade, projectGroup));
		}
	}
}