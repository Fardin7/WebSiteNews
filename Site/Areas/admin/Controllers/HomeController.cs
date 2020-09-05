using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace   Site.Area.admin.Controllers
{
    public class HomeController : BaseController
    //: BaseController
    {
        public ActionResult Index()
        {
            //var b = 0;
            //var k = 1 / b;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Error()
        {
            
            return View();
        }
        public ActionResult NotFound()
        {

            return View();
        }

    }
}