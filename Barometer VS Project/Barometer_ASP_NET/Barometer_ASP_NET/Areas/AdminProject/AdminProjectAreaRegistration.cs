using System.Web.Mvc;

namespace Barometer_ASP_NET.Areas.AdminProject
{
	public class AdminProjectAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "AdminProject";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"AdminProject_default",
				"Admin/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
