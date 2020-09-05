﻿using DAL;
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
    public class NewsController : Controller
    {

        private readonly Iservice<News> _service;
        private readonly INewsService _newsService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly INewsCategoryService _newscategoryService;
        private readonly INewsSubCategoryService _newssubCategoryService;
        private readonly INewsFileService _newsFileService;
        //  private Context db = new Context();
        public NewsController(Iservice<News> service, INewsService newsService, IUnitOfWork unitOfWork, ICategoryService categoryService,
          ISubCategoryService subCategoryService, INewsCategoryService newscategoryService, INewsSubCategoryService newsSubCategoryService
            , INewsFileService newsFileService)
        {
            this._service = service;
            this._newsService = newsService;
            this._unitOfWork = unitOfWork;
            this._categoryService = categoryService;
            this._subCategoryService = subCategoryService;
            this._newscategoryService = newscategoryService;
            this._newssubCategoryService = newsSubCategoryService;
            _newsFileService = newsFileService;
        }
        // GET: user/News
        public ActionResult Index(string type, string categoryname, string newscategoryname)
        {


            var newscategoryid = 0;
            var categoryid = 0;
            if (newscategoryname!=null)
            {
                newscategoryid= _newscategoryService.Get(q => q.Title == newscategoryname).FirstOrDefault().Id;

            }
            if (categoryname!=null)
            {
                categoryid= _categoryService.Get(q => q.Title == categoryname).FirstOrDefault().Id;

            }
          

            var newstype = -1;
            if (type == "اخبار")
            {
                newstype = 1;
                    //(int)Enum.Parse(typeof(NewsType), type);
            }
            else
            {
                newstype = 0;
            }
            var model = _newsService.ListNewsOfNewsCategoryAndCategory(newstype, categoryname, newscategoryname, 4);
            double pagecount = _service.Get(q => q.NewsSubCategory.NewsCategory.Title == newscategoryname || q.Subcategory.Category.Title == categoryname).Where(q => q.NewsType == newstype).Count();
            pagecount = Math.Ceiling(pagecount / 4);
            var newslist = new List<NewsIndexPaging>();
            foreach (var item in model)
            {
                newslist.Add(new NewsIndexPaging()
                {
                    NewsType = newstype,
                    Category = categoryid,
                    NewsCategory = newscategoryid,
                    Description = item.Description,
                    ImageAddress = item.ImageAddress,
                    PublishDate = item.PublishDate.Value,
                    NewsCategoryTitle=item.NewsSubCategory.NewsCategory.Title,
                    SubCategoryTitle=item.Subcategory.Title,
                    PageNumber=3,
                    Pages = pagecount,
                    Title = item.Title,
                    Url = Url.RouteUrl("news", new { type = type, cattegory = item.Subcategory.Title, newscattegory = item.NewsSubCategory.NewsCategory.Title, id = item.Title })



                });
            }
            return View(newslist);
        }
        public ActionResult IndexPartial(int type, string categoryname, string newscategoryname, string partialname, int count)
        {


            var model = _newsService.ListNewsOfNewsCategoryAndCategory(type, categoryname, newscategoryname, count);
            double pagecount = _service.Get(q => q.NewsSubCategory.NewsCategory.Title == newscategoryname && q.Subcategory.Title == categoryname && q.NewsType == type).Count();
            pagecount = Math.Ceiling((double)pagecount / count);

            ViewBag.pagecount = pagecount;
            return PartialView(partialname, model);
        }

        public string NewsOfNewsCategoryAndCategoryPaging(int type, int categoryname, int newscategoryname, int count, int pagenumber)
        {
            string urltype = "";
            if (type == 1)
            {
                urltype = "اخبار";
            }
            else
            {
                urltype = "مقاله";
            }
            // var allnews = _service.Get(q => q.NewsSubCategory.NewsCategoryId == newscategoryname && q.SubcategoryId == categoryname && q.NewsType == type).ToList();
            var model = _newsService.RelatedNewsPagin(type, categoryname, newscategoryname, count, pagenumber);
            var news = new List<NewsIndexPaging>();
            foreach (var item in model)
            {
                news.Add(new NewsIndexPaging()
                {
          
                    Description = item.Description,
                    ImageAddress = item.ImageAddress,
                    PublishDate = item.PublishDate.Value,
                    NewsCategoryTitle = item.NewsSubCategory.NewsCategory.Title,
                    SubCategoryTitle = item.Subcategory.Title,
                    PageNumber = 3,
                
                    Title = item.Title,
                    Url = Url.RouteUrl("news", new { type = type, cattegory = item.Subcategory.Title, newscattegory = item.NewsSubCategory.NewsCategory.Title, id = item.Title })



                });;
            }
            return JsonConvert.SerializeObject(news);
        }

        // GET: user/News/Details/5
        public ActionResult Details(string id, string type)
        {
            if (type == "اخبار")
            {
                type = "News";
            }
            else
            {
                type = "Article";
            }

            var typeint = (int)Enum.Parse(typeof(NewsType), type);
            return View(_newsService.GetByTitleAndType(id, typeint));

        }
        public ActionResult LastNews(int newscount, int newstype, string partialname)
        {

            var model = _service.Get(q => q.NewsType == newstype).OrderByDescending(q => q.PublishDate).Take(newscount).ToList();

            return PartialView(partialname, model);
        }

        public ActionResult NewsPagingView(int newstype)
        {
            var count = _service.Get(q => q.NewsType == newstype).Count();

            return PartialView("_newspaging", Math.Ceiling((double)count / 4));
        }

        public string NewsPaging(int newscount, int pagenumber, int newstype)
        {
            var model = _service.Get(q => q.NewsType == newstype).OrderByDescending(q => q.PublishDate).Take(newscount * pagenumber).Skip(newscount * (pagenumber - 1)).ToList();
            var list = new List<LastNews>();
            foreach (var item in model)
            {
                list.Add(new Site.LastNews()
                {
                    Title = item.Title,
                    ImageAddress = item.ImageAddress,
                    Url = Url.RouteUrl("news", new { type = "مقاله", cattegory = item.Subcategory.Title, newscattegory = item.NewsSubCategory.NewsCategory.Title, id = item.Title })


                });
            }
            string articls = JsonConvert.SerializeObject(list);


            //    model, Formatting.Indented, new JsonSerializerSettings
            //{
            //    PreserveReferencesHandling = PreserveReferencesHandling.All
            //});


            return articls;
        }



    }
}