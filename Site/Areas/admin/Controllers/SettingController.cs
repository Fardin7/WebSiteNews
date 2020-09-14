﻿using DAL;
using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Area.admin.Controllers
{
    public class SettingController : Controller
    {

        private readonly Iservice<News> _service;
        private readonly INewsService _newsService;
        private readonly IUnitOfWork _unitOfWork;
        public SettingController(Iservice<News> service, INewsService newsService, IUnitOfWork unitOfWork)
        {
            this._service = service;
            this._newsService = newsService;
            this._unitOfWork = unitOfWork;


        }
        // GET: admin/Setting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTrendingNews()
        {

            return View("TrendingNews", _service.Get().OrderByDescending(q=>q.PublishDate));

        }

        public string SetTrendingNews(int newsid)
        {
            var news = _service.GetByID(newsid);
            if (news.IsTrend)
            {
                news.IsTrend = false;
            }
            else
            {
                news.IsTrend = true;
                news.TrendingDate = DateTime.Now;
            }
       
            _service.Update(news);
            var result = _unitOfWork.Complete();
            if (result==1)
            {
                return "انجام شد";
            }
            return "خطای رخ داده است";
           
        }
        public string SetNewsTrend(int Id)
        {

            return "";
        }

    }
}