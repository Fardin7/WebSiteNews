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
    public class HomeController : Controller
    {
        private readonly Iservice<News> _service;
        private readonly INewsService _newsService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly INewsCategoryService _newscategoryService;
        private readonly INewsSubCategoryService _newssubCategoryService;
        private readonly INewsFileService _newsFileService;
        public HomeController(Iservice<News> service, INewsService newsService, IUnitOfWork unitOfWork, ICategoryService categoryService,
         ISubCategoryService subCategoryService, INewsCategoryService newscategoryService, INewsSubCategoryService newsSubCategoryService
           , INewsFileService newsFileService)
        {
            this._service = service;
        }
        public ActionResult Index()
        {
            return View();
        }

   
      

    }
}
