using System.Web.Optimization;

namespace HoneyWedding
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

            bundles.Add(new ScriptBundle("~/bundles/mdl").Include(
                        "~/Scripts/material.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/prefixfree").Include(
                        "~/Scripts/prefixfree.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            bundles.Add(new ScriptBundle("~/bundles/fastdom").Include(
                        "~/Scripts/fastdom/fastdom.js",
                        //"~/Scripts/fastdom/fastdom.min.js",
                        "~/Scripts/fastdom/fastdom-strict.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.custom.js",
                      "~/Scripts/respond.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/gsap").Include(
                      "~/Content/greensock-js/src/minified/TweenMax.min.js",
                      "~/Content/greensock-js/src/minified/plugins/ScrollToPlugin.min.js",
                      "~/Content/scrollmagic/minified/ScrollMagic.min.js",
                      "~/Content/scrollmagic/minified/plugins/animation.gsap.min.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    //"~/Content/themes/nvapps/bootstrap.css",
                    "~/Content/mdl.css",
                    "~/Content/form_validation.css",
                    "~/Content/site.css",
                    "~/Content/table__fixed-headers.css"));

            bundles.Add(new StyleBundle("~/Content/parallax_css").Include(
                      "~/Content/parallax.css"));

            bundles.Add(new StyleBundle("~/Content/modernizr_css").Include(
                      "~/Content/modernizr.css"));
        }
    }
}
