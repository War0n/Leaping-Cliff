using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.Models
{
	public class Template
	{
		public string TemplateName { get; set; }
		public List<string> TemplateCriterium { get; set; }

		public Template(string name)
		{
			TemplateName = name;
		}
	}
}