using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Site.RouteTranslator
{
    public class GetSEOFriendlyRoute : Route
    {
        public GetSEOFriendlyRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler)
        {
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);

            if (routeData != null)
            {
                //if (routeData.Values.ContainsKey("id"))
                var controller = routeData.Values["controller"].ToString();
              
                    routeData.Values["controller"] = "Article";
                
               
                    routeData.Values["action"] = "Details";
                
           
            }

            return routeData;
        }


    }
}