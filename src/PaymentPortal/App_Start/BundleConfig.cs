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

            bundles.Add(new ScriptBundle("~/bundles/accessible-autocomplete")
                .Include("~/Scripts/accessible-autocomplete.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/payment").Include(
                "~/Scripts/AppScripts/payments.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/accessible-autocomplete.min.css",
                "~/Content/app.css"));

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