﻿using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Iservice<Category> _service;
        private readonly ICategoryService _categoryService;
        private readonly INewsCategoryService _newscategoryService;
        private readonly INewsSubCategoryService _newssubCategoryService;
        private readonly INewsFileService _newsFileService;
        //  private Context db = new Context();
        public CategoryController(Iservice<Category> service, INewsService newsService, ICategoryService categoryService,
          ISubCategoryService subCategoryService, INewsCategoryService newscategoryService, INewsSubCategoryService newsSubCategoryService
            , INewsFileService newsFileService)
        {
            _service = service;
            _categoryService = categoryService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryList(int count)
        {
            var model = _service.Get().Take(count).ToList();
            return PartialView("_CategoryList", model);
        }

        public ActionResult LastNewsOfCategory(int newscount, int newstype)
        {
            var query = _categoryService.LastNewsOfCategory(newstype);
            var model = (from news in query
                         select new NewsOfCategory
                         {
                             Title = news.Key,
                             News = news.Take(newscount)
                         }).ToList();



            return PartialView("_ListNewsOfCategory", model);
        }

    }
}