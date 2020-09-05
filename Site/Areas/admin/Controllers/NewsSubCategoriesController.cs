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
        public NewsSubCategoriesController(INewsSubCategoryService newssubcategoryService, IUnitOfWork unitOfWork, Iservice<NewsSubCategory> service)
        {
            this._newssubcategoryService = newssubcategoryService;
            this._unitOfWork = unitOfWork;
            this._service = service;
        }


        // GET: Categories
        public ActionResult Index()
        {
            return View(_newssubcategoryService.Get());
        }


        // GET: NewsSubCategories/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NewsSubCategory newsSubCategory = db.NewsSubCategories.Find(id);
        //    if (newsSubCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(newsSubCategory);
        //}

        //// GET: NewsSubCategories/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: NewsSubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NewsSubCategory newsSubCategory = db.NewsSubCategories.Find(id);
        //    if (newsSubCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.NewsCategoryId = new SelectList(db.NewsCategories, "Id", "Title", newsSubCategory.NewsCategoryId);
        //    ViewBag.NewsSubCategoryId = new SelectList(db.NewsSubCategories, "Id", "Title", newsSubCategory.NewsSubCategoryId);
        //    return View(newsSubCategory);
        //}

        //// POST: NewsSubCategories/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Title,IsActive,CreateDate,NewsCategoryId,NewsSubCategoryId")] NewsSubCategory newsSubCategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(newsSubCategory).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.NewsCategoryId = new SelectList(db.NewsCategories, "Id", "Title", newsSubCategory.NewsCategoryId);
        //    ViewBag.NewsSubCategoryId = new SelectList(db.NewsSubCategories, "Id", "Title", newsSubCategory.NewsSubCategoryId);
        //    return View(newsSubCategory);
        //}

        //// GET: NewsSubCategories/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NewsSubCategory newsSubCategory = db.NewsSubCategories.Find(id);
        //    if (newsSubCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(newsSubCategory);
        //}

        //// POST: NewsSubCategories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    NewsSubCategory newsSubCategory = db.NewsSubCategories.Find(id);
        //    db.NewsSubCategories.Remove(newsSubCategory);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
