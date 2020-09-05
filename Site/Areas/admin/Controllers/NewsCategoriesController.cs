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
    public class NewsCategoriesController : BaseController
    {
        private INewsCategoryService _newscategoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Iservice<NewsCategory> _service;
        public NewsCategoriesController(INewsCategoryService newscategoryService, IUnitOfWork unitOfWork, Iservice<NewsCategory> service)
        {
            this._newscategoryService = newscategoryService;
            this._unitOfWork = unitOfWork;
            this._service = service;
        }


        // GET: Categories
        public ActionResult Index()
        {
            return View(_newscategoryService.Get());
        }

        // GET: NewsCategories/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NewsCategory newsCategory = db.NewsCategories.Find(id);
        //    if (newsCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(newsCategory);
        //}

        // GET: NewsCategories/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: NewsCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public string Create([Bind(Include = "Id,Title,IsActive,CreateDate")] NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<object> newsCategores = new List<object>();
                _service.Insert(newsCategory);
                _unitOfWork.Complete(true);
                newsCategores.AddRange(_newscategoryService.Get().Select(z =>
                    new Category
                    {
                        Id = z.Id,
                        Title = z.Title
                    }

                    ).ToList());

                return serializer.Serialize(newsCategores);
            }

            return "error";
        }

        // GET: NewsCategories/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        NewsCategory newsCategory = db.NewsCategories.Find(id);
    //        if (newsCategory == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(newsCategory);
    //    }

    //    // POST: NewsCategories/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "Id,Title,IsActive,CreateDate")] NewsCategory newsCategory)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(newsCategory).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        return View(newsCategory);
    //    }

    //    // GET: NewsCategories/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        NewsCategory newsCategory = db.NewsCategories.Find(id);
    //        if (newsCategory == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(newsCategory);
    //    }

    //    // POST: NewsCategories/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        NewsCategory newsCategory = db.NewsCategories.Find(id);
    //        db.NewsCategories.Remove(newsCategory);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    }
}
