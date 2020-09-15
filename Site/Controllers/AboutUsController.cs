using DAL;
using Model;
using Newtonsoft.Json;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class AboutUsController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }
      
    }
}