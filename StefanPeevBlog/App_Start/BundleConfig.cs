using System.Web;
using System.Web.Optimization;

namespace StefanPeevBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/IndexPageScripts").Include(
                        "~/Scripts/GetUserBriefInfoPopUp.js"));

            bundles.Add(new ScriptBundle("~/bundles/imagepreview").Include(
                        "~/Scripts/ImagePreview.js"));

            bundles.Add(new ScriptBundle("~/bundles/datePicker").Include(
                        "~/Scripts/moment.min.js",
                        "~/Scripts/bootstrap-datetimepicker.min.js"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                 "~/Content/bootstrap-datetimepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/images").Include(
                        "~/Scripts/drag-and-dropping.js"));

            bundles.Add(new ScriptBundle("~/bundles/ClickOnTagToShow").Include(
                        "~/Scripts/ClickOnTagToShow.js",
                        "~/Scripts/DeleteGlyphAjaxDeleteAction.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/HomeIndex").Include(
                        "~/Content/HomeIndexPage.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/Site.css"
                        //"~/Content/themes/base/jquery-ui.min.css",
                        ));
        }
    }
}
