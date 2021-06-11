using log4net;
using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Area.admin.Controllers
{
    public class HomeController : BaseController

    {
        private readonly Iservice<News> _service;
        public HomeController(Iservice<News> service)
        {
            Iservice<News> _service = service;

        }
        public ActionResult Index()
        {
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