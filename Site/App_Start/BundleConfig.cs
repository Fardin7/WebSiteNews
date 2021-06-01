using System.Web;
using System.Web.Optimization;

namespace Site
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/customdropdownjs").Include(
                 "~/Scripts/CustomJs/DropDownList.js"));
            bundles.Add(new ScriptBundle("~/bundles/Paging").Include(
               "~/Scripts/CustomJs/Paging.js"));

            
            bundles.Add(new ScriptBundle("~/bundles/sitescriptfiles").Include(
                  "~/WebSiteContent/assets/js/vendor/modernizr-3.5.0.min.js",

     "~/WebSiteContent/assets/js/vendor/jquery-1.12.4.min.js",
    "~/WebSiteContent/assets/js/popper.min.js",
     "~/WebSiteContent/assets/js/bootstrap.min.js",

     "~/WebSiteContent/assets/js/jquery.slicknav.min.js",

    "~/WebSiteContent/assets/js/owl.carousel.min.js",
    "~/WebSiteContent/assets/js/slick.min.js",

   "~/WebSiteContent/assets/js/gijgo.min.js",

     "~/WebSiteContent/assets/js/wow.min.js",
    "~/WebSiteContent/assets/js/animated.headline.js",
     "~/WebSiteContent/assets/js/jquery.magnific-popup.js",

   "~/WebSiteContent/assets/js/jquery.ticker.js",
    "~/WebSiteContent/assets/js/site.js",


     "~/WebSiteContent/assets/js/jquery.scrollUp.min.js",
     "~/WebSiteContent/assets/js/jquery.nice-select.min.js",
     "~/WebSiteContent/assets/js/jquery.sticky.js",


     "~/WebSiteContent/assets/js/contact.js",
     "~/WebSiteContent/assets/js/jquery.form.js",
     "~/WebSiteContent/assets/js/jquery.validate.min.js",
     "~/WebSiteContent/assets/js/mail-script.js",
    "~/WebSiteContent/assets/js/jquery.ajaxchimp.min.js",


   "~/WebSiteContent/assets/js/plugins.js",
     "~/WebSiteContent/assets/js/main.js"



                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/sitecssfiles").Include(
                     "~/WebSiteContent/assets/css/rtl/bootstrap.min.css",
    "~/WebSiteContent/assets/css/rtl/owl.carousel.min.css",
      "~/WebSiteContent/assets/css/flaticon.css",
    "~/WebSiteContent/assets/css/rtl/slicknav.css",
     "~/WebSiteContent/assets/css/rtl/animate.min.css",
 "~/WebSiteContent/assets/css/rtl/magnific-popup.css",
     "~/WebSiteContent/assets/css/fontawesome-all.min.css",
    "~/WebSiteContent/assets/css/rtl/themify-icons.css",
   "~/WebSiteContent/assets/css/rtl/slick.css",
     "~/WebSiteContent/assets//css/rtl/nice-select.css",
     "~/WebSiteContent/assets/css/rtl/style.css",
  "~/WebSiteContent/assets/css/rtl/ticker-style.css"
                    ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
