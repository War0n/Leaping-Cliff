using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barometer_ASP_NET.Models
{
	public class ProjectGroup
	{
		public string GroupName { get; set; }
		public List<Student> Students { get; set; }

		public ProjectGroup(string groupname)
		{
			GroupName = groupname;
			Students = new List<Student>();
		}
	}
}
