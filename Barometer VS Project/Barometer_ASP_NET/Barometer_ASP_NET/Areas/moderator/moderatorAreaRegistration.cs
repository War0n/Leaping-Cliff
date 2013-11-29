﻿using System.Web.Mvc;

namespace Barometer_ASP_NET.Areas.moderator
{
	public class moderatorAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "moderator";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"moderator_default",
				"moderator/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
