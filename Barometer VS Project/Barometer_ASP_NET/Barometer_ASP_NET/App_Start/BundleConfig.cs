using System.Web;
using System.Web.Optimization;

namespace Barometer_ASP_NET
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.smartresize.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.unobtrusive*",
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

            //Eigen stylesheets 
			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/css/bootstrap.css", "~/Content/css/default.css"));

            //Eigen JavaScript files
			bundles.Add(new ScriptBundle("~/Content/js").Include(
                        "~/Content/js/bootstrap-min.js", "~/Content/js/baro.widget.js", "~/Content/js/bootbox.min.js"));
		}
	}
}