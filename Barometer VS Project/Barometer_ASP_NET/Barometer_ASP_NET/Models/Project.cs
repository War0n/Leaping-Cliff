using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.Models
{
	public class Project
	{
		public string Name { get; set; }
		public List<ProjectGroup> ProjectGroups { get; set; }

		public Project(string name)
		{
			Name = name;
			ProjectGroups = new List<ProjectGroup>();
		}
	}
}