using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        private readonly ICategoryService _categoryservice;
        public SubcategoriesController(ISubCategoryService subcategoryService, IUnitOfWork unitOfWork, Iservice<Subcategory> service, ICategoryService categoryservice)
        {
            this._subcategoryService = subcategoryService;
            this._unitOfWork = unitOfWork;
            this._service = service;
            this._categoryservice = categoryservice;
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
            ViewBag.CategoryId = new SelectList(_categoryservice.Get(), "Id", "Title");
            ViewBag.SubcategoryId = new SelectList(_subcategoryService.Get(), "Id", "Title");

            return View("Create");
        }
        public ActionResult CreatePartial()
        {
            return PartialView("SubCategoryCreatePartial");
            //
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
                if (TempData["SubCategoryAddressImage"] != null)
                {
                    var address = TempData["SubCategoryAddressImage"].ToString();
                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["SubCategoryImageFile"];
                    subcategory.ImageAddress = address;

                    file.SaveAs(Server.MapPath(address));
                    TempData["SubCategoryAddressImage"] = null;
                    TempData["SubCategoryImageFile"] = null;

                }
                else
                {
                    subcategory.ImageAddress = "/SubCategoryImage/SubCategoryDefaultImage.jpg";
                }
        
                _service.Insert(subcategory);
                _unitOfWork.Complete(true);
                subcategorylis.AddRange(_subcategoryService.Get().Where(q=>q.CategoryId== subcategory.CategoryId).Select(z =>
                    new Category
                    {
                        Id = z.Id,
                        Title = z.Title,
                        ImageAddress=z.ImageAddress
                    }

                    ).ToList());

                return serializer.Serialize(subcategorylis);
            }
            return "error";
        }

        // GET: Subcategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                Subcategory subcategory = _service.GetByID(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_categoryservice.Get(), "Id", "Title", subcategory.CategoryId);
            ViewBag.SubcategoryId = new SelectList(_subcategoryService.Get(), "Id", "Title", subcategory.SubcategoryId);
            return View(subcategory);
        }

        //// POST: Subcategories/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsActive,ImageAddress,CategoryId,SubcategoryId")] Subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
              
                if (TempData["SubCategoryAddressImage"] != null)
                {
                    var address = TempData["SubCategoryAddressImage"].ToString();
                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["SubCategoryImageFile"];
                    subcategory.ImageAddress = address;

                    file.SaveAs(Server.MapPath(address));
                    TempData["SubCategoryAddressImage"] = null;
                    TempData["SubCategoryImageFile"] = null;

                }
                else
                {
                    subcategory.ImageAddress = "/SubCategoryImage/SubCategoryDefaultImage.jpg";
                }
                _service.Update(subcategory);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_categoryservice.Get(), "Id", "Title", subcategory.CategoryId);
            ViewBag.SubcategoryId = new SelectList(_subcategoryService.Get(), "Id", "Title", subcategory.SubcategoryId);
            return View(subcategory);
        }

        //// GET: Subcategories/Delete/5
        [HttpPost]
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

        [HttpPost]
        public void SaveSubCategoryImage()
        {

            HttpFileCollectionBase files = Request.Files;

            if (files.Count >= 1)
            {
                HttpPostedFileBase file = files[0];
                string actaulfilename;

                // Checking for Internet Explorer  
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    actaulfilename = testfiles[testfiles.Length - 1];
                }
                else
                {
                    actaulfilename = Path.GetFileName(file.FileName);
                }
                var filename = DateTime.Now.ToString("yyyyMMdd") + "-" + actaulfilename.Trim();
                var filetype = Path.GetExtension(file.FileName);

                TempData["SubCategoryAddressImage"] = "/SubCategoryImage/" + filename;
                TempData["SubCategoryImageFile"] = file;

            }

        }

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
