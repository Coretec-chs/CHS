using System.Web;
using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.form*",
                        "~/Scripts/jquery.blockUI.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/validation.js",
                        "~/Scripts/MvcFoolproofJQueryValidation.js",

                        "~/Scripts/moment.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.js"
                       //, "~/Scripts/toastrOptions.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/ImageUpload").Include(
                        "~/Scripts/Site/ImageUpload.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                       //"~/Content/global/vendor/bootstrap/bootstrap.min.js",
                       "~/Content/global/js/core.min.js",
                       "~/Content/global/assets/js/site.min.js",
                       "~/Content/global/vendor/animsition/animsition.min.js",
                       "~/Content/global/vendor/asscroll/jquery-asScroll.min.js",
                       "~/Content/global/vendor/mousewheel/jquery.mousewheel.min.js",
                       "~/Content/global/vendor/asscrollable/jquery.asScrollable.all.min.js",
                       "~/Content/global/vendor/ashoverscroll/jquery-asHoverScroll.min.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                        "~/Scripts/jquery.bonnet.ajax-dropdownlist.js",
                         //"~/Content/global/vendor/select2/select2.min.js",
                         "~/Scripts/spin.min.js",
                        "~/Scripts/select2.min.js",
                        "~/Content/global/vendor/bootstrap-select/bootstrap-select.min.js",
                        "~/Scripts/sweetalert.min.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Content/DataTables/datatables.js",
                        "~/Content/global/vendor/icheck/icheck.min.js",
                        "~/Content/global/vendor/switchery/switchery.min.js",
                        "~/Content/global/vendor/intro-js/intro.min.js",
                        "~/Content/global/vendor/screenfull/screenfull.min.js",
                        "~/Content/global/vendor/slidepanel/jquery-slidePanel.min.js",
                        "~/Content/global/vendor/aspieprogress/jquery-asPieProgress.min.js",
                        "~/Content/global/vendor/matchheight/jquery.matchHeight-min.js",
                        "~/Content/global/vendor/jquery-selective/jquery-selective.min.js"                        
                        ));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                       "~/Content/global/js/components/bootstrap-select.min.js",
                       "~/Content/global/js/components/icheck.min.js",
                       "~/Content/global/assets/js/sections/menu.min.js",
                       "~/Content/global/assets/js/sections/menubar.min.js",
                       "~/Content/global/assets/js/sections/gridmenu.min.js",
                       "~/Content/global/assets/js/sections/sidebar.min.js",
                       "~/Content/global/js/configs/config-colors.min.js",
                       "~/Content/global/assets/js/configs/config-tour.min.js",
                       "~/Content/global/js/components/asscrollable.min.js",
                       "~/Content/global/js/components/animsition.min.js",
                       "~/Content/global/js/components/slidepanel.min.js",
                       "~/Content/global/js/components/switchery.min.js",
                       "~/Content/global/js/components/matchheight.min.js",
                       "~/Content/global/js/components/aspieprogress.min.js",
                       "~/Content/global/js/components/jquery-placeholder.min.js",
                       "~/Content/global/js/components/input-group-file.min.js",
                       "~/Content/global/js/components/buttons.min.js",
                        "~/Scripts/Site/main.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*",
                        "~/Content/global/vendor/breakpoints/breakpoints.min.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/global/css/bootstrap.min3f0d.css",
                        "~/Content/global/css/bootstrap-extend.min3f0d.css",
                        "~/Content/global/assets/css/site.min3f0d.css"));

            bundles.Add(new StyleBundle("~/Content/Plugins").Include(
                       //"~/Content/global/vendor/bootstrap-select/bootstrap-select.min3f0d.css",
                       "~/Content/DataTables/datatables.min.css",
                       "~/Content/css/select2.css",
                       "~/Content/css/select2-bootstrap.css",
                       "~/Content/global/vendor/icheck/icheck.min3f0d.css",
                       "~/Content/global/vendor/animsition/animsition.min3f0d.css",
                       "~/Content/global/vendor/asscrollable/asScrollable.min3f0d.css",
                       "~/Content/global/vendor/switchery/switchery.min3f0d.css",
                       "~/Content/global/vendor/intro-js/introjs.min3f0d.css",
                       "~/Content/global/vendor/slidepanel/slidePanel.min3f0d.css",
                       "~/Content/global/vendor/flag-icon-css/flag-icon.min3f0d.css",
                       "~/Content/global/vendor/aspieprogress/asPieProgress.min3f0d.css",
                       "~/Content/global/vendor/jquery-selective/jquery-selective.min3f0d.css",
                       "~/Content/global/vendor/jquery-wizard/jquery-wizard.min3f0d.css",
                       "~/Content/global/vendor/formvalidation/formValidation.min3f0d.css",
                       "~/Content/bootstrap-datetimepicker.min.css",
                       "~/Content/sweetalert.css",
                       "~/Content/toastr.min.css",
                       "~/Content/dropify/dropify.min.css",
                       "~/Content/global/assets/examples/css/dashboard/team.min3f0d.css"
                       //,"~/Content/SmartWizard/smart_wizard.css",
                       //"~/Content/SmartWizard/smart_wizard_theme_arrows.css"
                       ));

            bundles.Add(new StyleBundle("~/Content/Font").Include(
                        "~/Content/global/fonts/web-icons/web-icons.min3f0d.css", new CssRewriteUrlTransform())
                        .Include("~/Content/global/fonts/brand-icons/brand-icons.min3f0d.css", new CssRewriteUrlTransform())
                        .Include("~/Content/global/fonts/glyphicons/glyphicons.min3f0d.css", new CssRewriteUrlTransform())
                        .Include("~/Content/global/fonts/font-awesome/font-awesome.min3f0d.css", new CssRewriteUrlTransform()));

        }
    }
}
