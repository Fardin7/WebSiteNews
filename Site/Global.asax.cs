using log4net;
using Site.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        protected void Application_Start()
        {
            CultureHelper.CultureInfo = new System.Globalization.CultureInfo("fa-IR");
            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_Error()
        {
            //HttpContext httpContext = HttpContext.Current;
            //if (httpContext != null)
            //{
            //RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
            ///* When the request is ajax the system can automatically handle a mistake with a JSON response. 
            //   Then overwrites the default response */
            //if (requestContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    httpContext.Response.Clear();
            //    string controllerName = requestContext.RouteData.GetRequiredString("controller");
            //    IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
            //    IController controller = factory.CreateController(requestContext, controllerName);
            //    ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);

            //    JsonResult jsonResult = new JsonResult
            //    {
            //        Data = new { success   false, serverError = "500" },
            //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //    };
            //    jsonResult.ExecuteResult(controllerContext);
            //    httpContext.Response.End();
            //}
            //else
            //{
            //    httpContext.Response.Redirect("~/Error");
            //}
            var ex = Server.GetLastError();
            var statuscode = 0;
            if (ex is HttpException)
            {
                statuscode = ((HttpException)ex).GetHttpCode();
            }
            var type = ex.GetType().Name;
            var message = ex.Message;

            Logger.Error(string.Format("External Error...{0}", message), ex);
           // Server.ClearError();
            //if (statuscode == 404)
            //{
            //    Response.Redirect("/Home/NotFound");
            //}
            //else
            //{
            //    Response.Redirect("/Home/Error");
            //}

        }

    }
}
