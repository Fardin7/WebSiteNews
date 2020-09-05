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
    public class SubcategoriesController : BaseController
    {
        private ISubCategoryService _subcategoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Iservice<Subcategory> _service;
        public SubcategoriesController(ISubCategoryService subcategoryService, IUnitOfWork unitOfWork, Iservice<Subcategory> service)
        {
            this._subcategoryService = subcategoryService;
            this._unitOfWork = unitOfWork;
            this._service = service;
        }

        // GET: Subcategories
        public ActionResult Index()
        {
            //var subcategories = db.Subcategories.Include(s => s.Category).Include(s => s.Subcategory2);
            return View(_subcategoryService.Get());
        }

        //// GET: Subcategories/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subcategory subcategory = db.Subcategories.Find(id);
        //    if (subcategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subcategory);
        //}

        // GET: Subcategories/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: Subcategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public string Create([Bind(Include = "Id,Title,IsActive,ImageAddress,CategoryId,SubcategoryId")] Subcategory subcategory)
        {
                if (ModelState.IsValid)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<object> subcategorylis = new List<object>();
                _service.Insert(subcategory);
                _unitOfWork.Complete(true);
                subcategorylis.AddRange(_subcategoryService.Get().Where(q=>q.CategoryId== subcategory.CategoryId).Select(z =>
                    new Category
                    {
                        Id = z.Id,
                        Title = z.Title
                    }

                    ).ToList());

                return serializer.Serialize(subcategorylis);
            }
            return "error";
        }

        //// GET: Subcategories/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subcategory subcategory = db.Subcategories.Find(id);
        //    if (subcategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", subcategory.CategoryId);
        //    ViewBag.SubcategoryId = new SelectList(db.Subcategories, "Id", "Title", subcategory.SubcategoryId);
        //    return View(subcategory);
        //}

        //// POST: Subcategories/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Title,IsActive,ImageAddress,CategoryId,SubcategoryId")] Subcategory subcategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(subcategory).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", subcategory.CategoryId);
        //    ViewBag.SubcategoryId = new SelectList(db.Subcategories, "Id", "Title", subcategory.SubcategoryId);
        //    return View(subcategory);
        //}

        //// GET: Subcategories/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subcategory subcategory = db.Subcategories.Find(id);
        //    if (subcategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subcategory);
        //}

        //// POST: Subcategories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Subcategory subcategory = db.Subcategories.Find(id);
        //    db.Subcategories.Remove(subcategory);
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
