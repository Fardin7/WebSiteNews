using Site.RouteTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Site
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                      "newsofallnewscategory",
                      "همه دسته بندی ها-{type}-{newscount}",
                       new { controller = "NewsCategory", action = "IndexNewsOfNewsCategory" },
                       namespaces: new[] { "Site.Controllers" }


                  );

            routes.MapRoute(
                        "news",
                        "{cattegory}-{newscattegory}-{id}-{type}",
                         new { controller = "News", action = "Details" },
                         namespaces: new[] { "Site.Controllers" }


                    );
            routes.MapRoute(
                        "newsofnewscategory",
                        " سرار جهان-{type}-{newscategoryname}",
                         new { controller = "News", action = "Index" },
                         namespaces: new[] { "Site.Controllers" }
                    );
            routes.MapRoute(
                          "newsofcategory",
                          "دسته های خبری-{type}-{categoryname}",
                           new { controller = "News", action = "Index" },
                           namespaces: new[] { "Site.Controllers" }
                      );


            routes.MapRoute(
                        "newsofnewscategoryandcategory",
                        "{type}-{categoryname}-{newscategoryname}",
                         new { controller = "News", action = "Index", categoryname = UrlParameter.Optional, newscategoryname = UrlParameter.Optional },
                         namespaces: new[] { "Site.Controllers" }
                    );

            routes.MapRoute(
                        "contactus",
                        "تماس با ما",
                         new { controller = "Contact", action = "Index" },
                         namespaces: new[] { "Site.Controllers" }
                    );
            routes.MapRoute(
                       "aboutus",
                       "درباره ما",
                        new { controller = "AboutUs", action = "Index" },
                        namespaces: new[] { "Site.Controllers" }
                   );
            routes.MapRoute(
name: "Default",
url: "{controller}/{action}/{id}",
defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
namespaces: new[] { "Site.Controllers" }
);

            //  routes.Add("DetailsDetails", new GetSEOFriendlyRoute("مقاله/جامعه/{id}",
            //new RouteValueDictionary(new { controller = "Article", action = "Details" }),
            //new MvcRouteHandler()));

            //        routes.MapRoute(
            //  "Catchall",
            //  "{*anything}",
            //  new { controller = "Error", action = "Missing" }
            //);

        }
    }
}
