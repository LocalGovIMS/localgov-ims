using System.Web.Optimization;

namespace PaymentPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/accessible-autocomplete")
                .Include("~/Scripts/accessible-autocomplete.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/payment").Include(
                "~/Scripts/AppScripts/payments.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/accessible-autocomplete.min.css",
                "~/Content/app.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectable.css",
                "~/Content/themes/base/jquery.ui.accordion.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css"));

            if (IncludeGoogleAnalytics)
            {
                bundles.Add(new ScriptBundle("~/bundles/GoogleAnalytics").Include(
                    "~/Scripts/GoogleAnalytics/googleAnalytics.js"));
            }
            else
            {
                bundles.Add(new ScriptBundle("~/bundles/GoogleAnalytics"));
            }
        }

        public static bool IncludeGoogleAnalytics
        {
            get
            {
                if (bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["IncludeGoogleAnalytics"], out bool include))
                    return include;
                else
                    return false;
            }
        }
    }
}