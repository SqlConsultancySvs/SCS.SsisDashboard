using System.Web;
using System.Web.Optimization;

namespace SCS.SsisDashboard.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryBlockUI").Include(
                        "~/Scripts/jquery.blockUI.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTable").Include(
                        "~/Scripts/DataTables/jquery.DataTables.js",
                        "~/Scripts/DataTables/dataTables.bootstrap.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-dialog.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                        "~/Scripts/app.js"
                        ));

            bundles.Add(new StyleBundle("~/content/fonts").Include(
                      "~/Content/fonts/font-awesome-4.1.0/css/font-awesome.min.css"
                      ));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/content/bootstrap/bootstrap.css",
                      "~/content/bootstrap/bootstrap-theme.css",
                      "~/content/bootstrap/bootstrap-dialog.min.css",
                       "~/content/bootstrap/dataTables.bootstrap.min.css",
                    "~/content/site.css"));


        }
    }
}
