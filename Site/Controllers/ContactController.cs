using DAL;
using Model;
using Newtonsoft.Json;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class ContactController : BaseController
    {
        private readonly Iservice<Contact> _service;
        private readonly IContactService _contactService;
        private readonly IUnitOfWork _unitOfWork;
        public ContactController(Iservice<Contact> service, IContactService contactService, IUnitOfWork unitOfWork)
        {
            _service = service;
            _contactService = contactService;
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View();
        }
         public ActionResult PartialContact()
        {
            
            return PartialView("_ContactUs");
        }
        [HttpPost]
        public string Create(Contact contact)
        {
            var textresult = "امکان درج پیام وجود ندارد!";
            contact.CreatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
             
                _service.Insert(contact);
                var result = _unitOfWork.Complete(true);
                
                if (result == 1)
                {
                    textresult = "پیام شما ارسال شد!";
                }
            }
           
            return JsonConvert.SerializeObject(new { message= textresult });
        }

       
    }
}