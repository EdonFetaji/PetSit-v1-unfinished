using System.Web;
using System.Web.Optimization;

namespace PetSit
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CSS bundle
            bundles.Add(new StyleBundle("~/css").Include(
                        "~/css/jquery-ui.css",    // jQuery UI styles
                        "~/css/main.css",         // Custom main styles
                        "~/css/Site.css",         // Site-wide styles
                        "~/css/tailwind.css"      // Tailwind CSS
                        ));

            // jQuery bundle
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.js"));  // Including jQuery UI script

            // Modernizr bundle (optional)
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Enable bundling and minification (optional)
            BundleTable.EnableOptimizations = true;
        }
    }
}
