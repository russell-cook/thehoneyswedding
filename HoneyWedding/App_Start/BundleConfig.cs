﻿using System.Web.Optimization;

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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.custom.js",
                      "~/Scripts/respond.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/themes/nvapps/bootstrap.css",
                      "~/Content/mdl.css",
                      "~/Content/modernizr.css",
                      "~/Content/site.css",
                      "~/Content/table__fixed-headers.css"));
        }
    }
}
