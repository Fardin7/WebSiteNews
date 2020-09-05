using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Areas.user
{
    public class userAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "user";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
          

            context.MapRoute(
                        "news",
                        "اخبار-{Newscat}-{Newssubcat}-{id}",
                         new { controller = "News", action = "Details"/*, cat = UrlParameter.Optional, subcat = UrlParameter.Optional */}

                    );
            context.MapRoute(
                        "article1",
                        "مقاله-{cat}-{subcat}-{id}",
                         new { controller = "Article", action = "Details"/*, cat = UrlParameter.Optional, subcat = UrlParameter.Optional, id = UrlParameter.Optional */}

                    );
            context.MapRoute(
         name: "Default",
         url: "user/{controller}/{action}/{id}",
         defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
     );

        }
    }
}