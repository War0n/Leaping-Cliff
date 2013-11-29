using System.Web.Mvc;

namespace Barometer_ASP_NET.Areas.admin
{
	public class adminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"admin_default",
				"Roles/admin/{controller}/{action}/{id}",
				new { action = "Dashboard", id = UrlParameter.Optional }
			);
		}
	}
}
