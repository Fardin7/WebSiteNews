using DAL;
using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
           
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult NotFound()
        {

            return View();
        }
        public ActionResult Error()
        {

            return View();
        }


    }
}
