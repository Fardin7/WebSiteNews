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
    public class CategoriesController : BaseController
    {
        private ICategoryService _categoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Iservice<Category> _service;
        public CategoriesController(ICategoryService categoryService, IUnitOfWork unitOfWork, Iservice<Category> service)
        {
            this._categoryService = categoryService;
            this._unitOfWork = unitOfWork;
            this._service = service;
        }


        // GET: Categories
        public ActionResult Index()
        {
            return View(_categoryService.Get());
        }

        // GET: Categories/Create
        public ActionResult CreatePartial()
        {
            return PartialView("CategoryCreatePartial");
            //
        }
        public ActionResult Create()
        {
            return View("Create");
            //
        }
        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public string Create([Bind(Include = "Id,Title,IsActive,ImageAddress")] Category category)
        {
            if (ModelState.IsValid)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<object> Categores = new List<object>();
                if (TempData["CategoryAddressImage"] != null)
                {
                    var address = TempData["CategoryAddressImage"].ToString();
                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["CategoryImageFile"];
                    category.ImageAddress = address;

                    file.SaveAs(Server.MapPath(address));
                    TempData["CategoryAddressImage"] = null;
                    TempData["CategoryImageFile"] = null;

                }
                else
                {
                    category.ImageAddress = "/CategoryImage/CategoryDefaultImage.jpg";
                }
                _service.Insert(category);
                _unitOfWork.Complete(true);




                Categores.AddRange(_categoryService.Get().Select(z =>
                    new Category
                    {
                        Id = z.Id,
                        Title = z.Title,
                        ImageAddress = z.ImageAddress

                    }

                    ).ToList());

                return serializer.Serialize(Categores);
            }

            return "error";
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _service.GetByID(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsActive,ImageAddress")] Category category)
        {
            if (ModelState.IsValid)
            {

                if (TempData["CategoryAddressImage"] != null)
                {
                    var address = TempData["CategoryAddressImage"].ToString();
                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["CategoryImageFile"];
                    category.ImageAddress = address;

                    file.SaveAs(Server.MapPath(address));
                    TempData["CategoryAddressImage"] = null;
                    TempData["CategoryImageFile"] = null;

                }
                else
                {
                    category.ImageAddress = "/CategoryImage/CategoryDefaultImage.jpg";
                }
                _service.Update(category);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
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
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<object> Categores = new List<object>();
                Categores.AddRange(_categoryService.Get().Select(z =>
                    new Category
                    {
                        Id = z.Id,
                        Title = z.Title,
                        ImageAddress = z.ImageAddress

                    }

                    ).ToList());

                return serializer.Serialize(Categores);
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        [HttpPost]
        public void SaveCategoryImage()
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

                TempData["CategoryAddressImage"] = "/CategoryImage/" + filename;
                TempData["CategoryImageFile"] = file;

            }

        }
    }
}
