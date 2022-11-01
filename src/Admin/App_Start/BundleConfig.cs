using System.Diagnostics.CodeAnalysis;
using System.Web.Optimization;

namespace Admin
{
    [ExcludeFromCodeCoverage]
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*")
                .Include("~/Scripts/globalize.js")
                .IncludeDirectory("~/Scripts/globalize/", "*.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/jqueryunobtrusive").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/semantic").Include(
                "~/Scripts/semantic.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/lodash").Include(
                "~/Scripts/lodash.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/accessible-autocomplete")
                .Include("~/Scripts/accessible-autocomplete.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/core")
                .IncludeDirectory("~/Scripts/App/Core/", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/app/shared")
                .IncludeDirectory("~/Scripts/App/Shared/", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/app/transaction")
                .IncludeDirectory("~/Scripts/App/Transaction/", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/app/transfer")
                .IncludeDirectory("~/Scripts/App/Transfer/", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/app/ereturn")
                .IncludeDirectory("~/Scripts/App/EReturn/", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/app/payment")
                .IncludeDirectory("~/Scripts/App/Payment/", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/app/template")
                .IncludeDirectory("~/Scripts/App/Template/", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/app/suspense")
                .IncludeDirectory("~/Scripts/App/Suspense/", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/app/account-holders")
                .IncludeDirectory("~/Scripts/App/AccountHolder/", "*.js", true));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"
                //"~/Scripts/respond.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/font-awesome.min.css",
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/themes/base/datepicker.css",
                "~/Content/bootstrap.css",
                "~/Content/accessible-autocomplete.min.css",
                "~/Content/app.css"));
        }
    }
}
