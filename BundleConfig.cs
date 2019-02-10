using System.Web;
using System.Web.Optimization;

namespace UtOpt
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery.easing.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js", 
                      "~/Scripts/sb-admin.js",
                      "~/Scripts/bootstrap.bundle.js", 
                      "~/Scripts/jquery.dataTables.js",
                      "~/Scripts/reports.js",
                      "~/Scripts/jquery.steps.js",
                    "~/Scripts/jquery.steps.regional.js",
                      "~/Scripts/rules.js",
                      "~/Scripts/dataTables.checkboxes.js",
                      "~/Scripts/bootstrap-modal.js",
                      "~/Scripts/bootstrap-modalmanager.js",
                      "~/Scripts/jquery-confirm.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css", "~/Content/sb-admin.css",
                      "~/Content/navbar.css",
                      "~/Content/all.css", 
                      "~/Content/dataTables.bootstrap4.css",
                      "~/Content/jquery.dataTables.css",
                      "~/Content/jquery.steps.css", 
                      "~/Content/awesome-bootstrap-checkbox.css",
                      "~/Content/vertical-menu.css",
                      "~/Content/components-lite.css",
                      "~/Content/bootstrap-extended.css",
                      "~/Content/dataTables.bootstrap.min.css",
                      "~/Content/jquery-confirm.css"));
        }
    }
}
