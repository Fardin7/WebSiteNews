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
using Site.CustomAuthorization;
using Business;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.UI;

namespace Site.Area.admin.Controllers
{
    public class NewsController : BaseController
    {
        private readonly Iservice<News> _service;
        private readonly INewsService _newsService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly INewsCategoryService _newscategoryService;
        private readonly INewsSubCategoryService _newssubCategoryService;
        private readonly INewsFileService _newsFileService;
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

        // GET: Articles 
        public ActionResult Index()
        {
            var article = _service.Get().ToList();
            return View(article);
        }

        // GET: Articles/Details/5
        public ActionResult Details(string id,int type)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News article = _newsService.GetByTitleAndType(id,type);
            if (article == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ViewNewsPartial", article);
        }

        // GET: Articles/Create
        
        public ActionResult Create()
        {

            try
            {
                FileManagement._fileManagement = null;
                ViewBag.CategoryId = new SelectList(_categoryService.Get(), "Id", "Title");
                ViewBag.NewsCategoryId = new SelectList(_newscategoryService.Get(), "Id", "Title");

            }
            catch (Exception ex)
            {
                return View();
            }
           
            return View();
        }
        [HttpGet]
        public void GenerateFile(string filename)
        {
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.TransmitFile(Server.MapPath("~/NewsFiles/" + filename));
            Response.End();


        }
        public bool DeleteImage(string addressimage)
        {
            try
            {
                System.IO.File.Delete(addressimage);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public bool DeleteFile()
        {
            try
            {
                FileManagement.DeleteFile(_newsFileService);
                _unitOfWork.Complete();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        [HttpPost]
        public void SaveNewFile()
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

                FileManagement.UploadFile(filetype, filename, file.ContentLength, Path.Combine(Server.MapPath("~/NewsFiles/"), filename), file);

            }

        }

        [HttpPost]
        public void SaveNewImage()
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

                TempData["NewsAddressImage"] = "/NewsImage/"+ filename ;
                TempData["NewsImageFile"] =file;

            }

        }
        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Body,KeyWord,PublishDate,IsActive,ImageAddress,SubcategoryId,NewsType,NewsSubcategoryId")] News news)
        {
            HttpFileCollectionBase files = Request.Files;
             if (news.PublishDate==null)
            {
                news.PublishDate = DateTime.Now;
            }
            if (news.SubcategoryId == 0)
            {

                ModelState.AddModelError("SubcategoryId", "please select subcategory");

            }
            if (news.NewsSubcategoryId == 0)
            {

                ModelState.AddModelError("NewsSubcategoryId", "please select NewsSubcategoryId");

            }
            if (ModelState.IsValid)
            {
                news.ApplicationUserId = User.Identity.GetUserId();
                if (TempData["NewsAddressImage"] != null)
                {
                    var address = TempData["NewsAddressImage"].ToString();
                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["NewsImageFile"];
                    news.ImageAddress = address;
                   
                    file.SaveAs(Server.MapPath(address));
                    TempData["NewsAddressImage"] = null;
                    TempData["NewsFile"] = null;
                   
                }
                else
                {
                    news.ImageAddress = "/NewsImage/DefaultImage.png";
                }
                _service.Insert(news);

                FileManagement.InsertFile(news, _newsFileService);
                _unitOfWork.Complete(true);
                return RedirectToAction("Create");
            }

            ViewBag.CategoryId = new SelectList(_categoryService.Get(), "Id", "Title");
            ViewBag.NewsCategoryId = new SelectList(_newscategoryService.Get(), "Id", "Title");
            return View("Create", news);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsService.Get(q => q.Id == id, null, "Subcategory.Category,NewsSubCategory.NewsCategory,NewsFiles").FirstOrDefault();
            if (news.NewsFiles.Count > 0)
            {
                var file = news.NewsFiles.FirstOrDefault();
                FileManagement.UploadFile(file.Type, file.Name, file.Size, Path.Combine(Server.MapPath("~/NewsFiles/"), file.Name), null, file.Id);

            }

            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_categoryService.Get(), "Id", "Title", news.Subcategory.CategoryId);
            ViewBag.SubcategoryId = new SelectList(_subCategoryService.Get().Where(q => q.CategoryId == news.Subcategory.CategoryId), "Id", "Title", news.SubcategoryId);

            ViewBag.NewsCategoryId = new SelectList(_newscategoryService.Get(), "Id", "Title", news.NewsSubCategory.NewsCategoryId);
            ViewBag.NewsSubcategoryId = new SelectList(_newssubCategoryService.Get().Where(q => q.NewsCategoryId == news.NewsSubCategory.NewsCategoryId), "Id", "Title", news.NewsSubcategoryId);

            return View(news);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult Edit([Bind(Include = "Id,Title,Description,Body,KeyWord,PublishDate,IsActive,ImageAddress,SubcategoryId,NewsType,NewsSubcategoryId")] News news)
        {

            if (ModelState.IsValid)
            {
                if (news.PublishDate == null)
                {
                    news.PublishDate = DateTime.Now;
                }
                news.ApplicationUserId = User.Identity.GetUserId();
                if (TempData["NewsAddressImage"] != null)
                {
                    var address = TempData["NewsAddressImage"].ToString();
                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["NewsImageFile"];
                    news.ImageAddress = address;
                    file.SaveAs(Server.MapPath(address));
                    TempData["NewsAddressImage"] = null;
                    TempData["NewsFile"] = null;
                   
                }
                else
                {
                    news.ImageAddress = "/NewsImage/DefaultImage.png";
                }
                _service.Update(news);

                FileManagement.InsertFile(news, _newsFileService);
                _unitOfWork.Complete(true);
                var hasfile = false;
                var filename = "";
                News editednews = _newsService.Get(q => q.Id == news.Id, null, "NewsFiles").FirstOrDefault();
                if (editednews.NewsFiles.Count > 0)
                {
                    hasfile = true;
                    var file = editednews.NewsFiles.FirstOrDefault();
                    filename = file.Name;
                    FileManagement.UploadFile(file.Type, file.Name, file.Size, Path.Combine(Server.MapPath("~/NewsFiles/"), file.Name), null, file.Id);

                }
                return Json(new { data = "1", existfile = hasfile, existfilename = filename });
            }

            ViewBag.CategoryId = new SelectList(_categoryService.Get(), "Id", "Title");
            ViewBag.NewsCategoryId = new SelectList(_newscategoryService.Get(), "Id", "Title");
            return Json(new { data = "0" });
        }



        // Get: Articles/Delete/5

        public bool Delete(int id)
        {
            var result = false;
            try
            {
                News article = _newsService.GetByID(id);
                _newsService.Delete(article);
                _unitOfWork.Complete();
                result = true;
            }
            catch (Exception)
            {

                throw new Exception();
            }
            return result;
        }

        public string FillNewsCategory(int id, string categorytype)
        {

            List<object> newsSubCategores = new List<object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonString = "";
            if (categorytype == "NewsSubcategoryId")
            {
                newsSubCategores.AddRange(_newssubCategoryService.Get().Where(q => q.NewsCategoryId == id).Select(z =>
                new NewsSubCategory
                {
                    Id = z.Id,
                    Title = z.Title

                }

                    )
                    .ToList());

                jsonString = serializer.Serialize(newsSubCategores);
            }
            else
            {
                newsSubCategores.AddRange(_subCategoryService.Get().Where(q => q.CategoryId == id).Select(z =>
                    new Subcategory
                    {
                        Id = z.Id,
                        Title = z.Title
                    }

                    ).ToList());

                jsonString = serializer.Serialize(newsSubCategores);
            }


            return jsonString;

        }
    }
}
