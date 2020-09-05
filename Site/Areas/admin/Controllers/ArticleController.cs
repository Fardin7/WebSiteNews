using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Model;
using Service.Interface;
using Site.CustomAuthorization;

namespace Site.Controllers
{
    public class ArticleController : Controller
    {
         private readonly Iservice<Article> _service;
        private readonly IArticleService _articleService;
        private readonly IUnitOfWork _unitOfWork;
        //  private Context db = new Context();
        public ArticleController(Iservice<Article>  service, IArticleService articleService, IUnitOfWork unitOfWork)
        {
            this._service = service;
            this._articleService = articleService;
            this._unitOfWork = unitOfWork;
        }

        // GET: Articles
        [CustomAuthorize]
        public ActionResult Index()
        {
            var gf = _articleService.GetCount();
             var article = _service.Get(/*includeProperties: "Title"*/);
            return View(article.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _articleService.GetByTitle(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        [CustomAuthorize]
        public ActionResult Create()
        {
           //ViewBag.ArticleSubcategoryId = new SelectList(db.ArticleSubcategory, "Id", "Title");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Body,KeyWord,PublishDate,IsActive,ImageAddress,ArticleSubcategoryId")] Article article)
        {
            if (ModelState.IsValid)
            {
               
                article.ArticleSubcategoryId = 1;
                _service.Insert(article);
               _unitOfWork.Complete(true);
                return RedirectToAction("Index");
            }

          //  ViewBag.ArticleSubcategoryId = new SelectList(db.ArticleSubcategory, "Id", "Title", article.ArticleSubcategoryId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _articleService.GetByID(id);
            if (article == null)
            {
                return HttpNotFound();
            }
           // ViewBag.ArticleSubcategoryId = new SelectList(_articleService.ArticleSubcategory, "Id", "Title", article.ArticleSubcategoryId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Body,KeyWord,PublishDate,IsActive,ImageAddress,ArticleSubcategoryId")] Article article)
        {
            if (ModelState.IsValid)
            {
                _articleService.Update(article);
                // db.Entry(article).State = EntityState.Modified;
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
           ViewBag.ArticleSubcategoryId = new SelectList(_articleService.Get(), "Id", "Title", article.ArticleSubcategoryId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _articleService.GetByID(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = _articleService.GetByID(id);
            _articleService.Delete(article);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

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
