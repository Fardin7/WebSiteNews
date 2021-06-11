using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DAL;
using Model;
using Service.Interface;
namespace Site.Area.admin.Controllers
{
    public class NewsSubCategoriesController : BaseController
    {
        private INewsSubCategoryService _newssubcategoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Iservice<NewsSubCategory> _service;
        private INewsCategoryService _newscategoryService;
        public NewsSubCategoriesController(INewsSubCategoryService newssubcategoryService, IUnitOfWork unitOfWork, Iservice<NewsSubCategory> service, INewsCategoryService newsCategoryService)
        {
            this._newssubcategoryService = newssubcategoryService;
            this._unitOfWork = unitOfWork;
            this._service = service;
            this._newscategoryService = newsCategoryService;
        }


        // GET: Categories
        public ActionResult Index()
        {
            return View(_newssubcategoryService.Get());
        }

        //// GET: NewsSubCategories/Create
        public ActionResult Create()
        {
            ViewBag.NewsCategoryId = new SelectList(_newscategoryService.Get(), "Id", "Title");
            ViewBag.NewsSubCategoryId = new SelectList(_newssubcategoryService.Get(), "Id", "Title");
            return View();
        }

        public ActionResult CreatePartial()
        {
            return PartialView("NewsSubCategoryCreatePartial");
        }
        
        // POST: NewsSubCategories/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public string Create([Bind(Include = "Id,Title,IsActive,CreateDate,NewsCategoryId,NewsSubCategoryId")] NewsSubCategory newsSubCategory)
        {
            if (ModelState.IsValid)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<object> newssubCategores = new List<object>();
                _service.Insert(newsSubCategory);
                _unitOfWork.Complete(true);
                newssubCategores.AddRange(_newssubcategoryService.Get().Where(q => q.NewsCategoryId == newsSubCategory.NewsCategoryId).Select(z =>
                        new Category
                        {
                            Id = z.Id,
                            Title = z.Title
                        }

                     ).ToList());

                return serializer.Serialize(newssubCategores);
            }

            return "error";
        }

        //// GET: NewsSubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsSubCategory newsSubCategory = _newssubcategoryService.GetByID(id);
            if (newsSubCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsCategoryId = new SelectList(_newscategoryService.Get(), "Id", "Title", newsSubCategory.NewsCategoryId);
            ViewBag.NewsSubCategoryId = new SelectList(_newssubcategoryService.Get(), "Id", "Title", newsSubCategory.NewsSubCategoryId);
            return View(newsSubCategory);
        }

        //// POST: NewsSubCategories/Edit/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsActive,CreateDate,NewsCategoryId,NewsSubCategoryId")] NewsSubCategory newsSubCategory)
        {
            if (ModelState.IsValid)
            {
                _service.Update(newsSubCategory);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.NewsCategoryId = new SelectList(_newscategoryService.Get(), "Id", "Title", newsSubCategory.NewsCategoryId);
            ViewBag.NewsSubCategoryId = new SelectList(_newssubcategoryService.Get(), "Id", "Title", newsSubCategory.NewsSubCategoryId);
            return View(newsSubCategory);
        }

        //// GET: NewsSubCategories/Delete/5
        public string Delete(int? id)
        {
            if (id == null)
            {
                return "id is not nullable";
            }
            try
            {
                _service.Delete(id);
                _unitOfWork.Complete();
                return "ok";

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
           
        }
    }
}
