﻿using System;
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

        // GET: NewsCategories/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreatePartial()
        {
            return PartialView("NewsCategoryCreatePartial");
        }
        
        // POST: NewsCategories/Create
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = _service.GetByID(id);
            if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

         // POST: NewsCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsActive,CreateDate")] NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
                _service.Update(newsCategory);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(newsCategory);
        }

        // GET: NewsCategories/Delete/5
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
