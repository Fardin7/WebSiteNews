using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Service.Interface;
using Service.Service;
using DAL;
using Model;
using Repository.Inerface;

using Repository.Repository;
using Unity.Injection;
using Unity.Lifetime;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;

namespace Site
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

           /// container.RegisterType<CustomAuthorize>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<UserManager<ApplicationUser>, ApplicationUserManager>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<INewsService, NewsService>();
            container.RegisterType<Iservice<News>, GenericService<News>>();
            container.RegisterType<IRepository<News>, GenericRepository<News>>();
            container.RegisterType<Iservice<Category>, GenericService<Category>>();
            container.RegisterType<IRepository<Category>, GenericRepository<Category>>();

            container.RegisterType<Iservice<NewsFile>, GenericService<NewsFile>>();
            container.RegisterType<IRepository<NewsFile>, GenericRepository<NewsFile>>();
            container.RegisterType<INewsFileService, NewsFileService>();
            container.RegisterType<INewsFileRepository, NewsFileRepository>();
            container.RegisterType<Iservice<Subcategory>, GenericService<Subcategory>>();
            container.RegisterType<IRepository<Subcategory>, GenericRepository<Subcategory>>();
            container.RegisterType<INewsRepository, NewsRepository>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<ISubCategoryService, SubCategoryService>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<ISubCategoryRepository, SubCategoryRepository>();

            container.RegisterType<Iservice<Comment>, GenericService<Comment>>();
            container.RegisterType<IRepository<Comment>, GenericRepository<Comment>>();

            container.RegisterType<ICommentService, CommentService>();
            container.RegisterType<ICommentRepository, CommentRepository>();

            container.RegisterType<Iservice<NewsLetter>, GenericService<NewsLetter>>();
            container.RegisterType<IRepository<NewsLetter>, GenericRepository<NewsLetter>>();

            container.RegisterType<INewsLetterService, NewsLetterService>();
            container.RegisterType<INewsLetterRepository, NewsLetterRepository>();



            container.RegisterType<Iservice<Contact>, GenericService<Contact>>();
            container.RegisterType<IRepository<Contact>, GenericRepository<Contact>>();

            container.RegisterType<IContactService, ContactService>();
            container.RegisterType<IContactRepository, ContactRepository>();

            container.RegisterType<Iservice<NewsSubCategory>, GenericService<NewsSubCategory>>();
            container.RegisterType<IRepository<NewsSubCategory>, GenericRepository<NewsSubCategory>>();
            container.RegisterType<Iservice<NewsCategory>, GenericService<NewsCategory>>();
            container.RegisterType<IRepository<NewsCategory>, GenericRepository<NewsCategory>>();
            container.RegisterType<INewsCategoryService, NewsCategoryService>();
            container.RegisterType<INewsSubCategoryService, NewsSubCategoryService>();
            container.RegisterType<INewsCategoryRepository, NewsCategoryRepository>();
            container.RegisterType<INewsSubCategoryRepository, NewsSubCategoryRepository>();

            container.RegisterType<DbContext, ApplicationDbContext>(
    new HierarchicalLifetimeManager());
            //container.RegisterType<UserManager<ApplicationUser>>(
            //    new HierarchicalLifetimeManager());
            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
            //    new HierarchicalLifetimeManager());
            container.RegisterType<IAuthenticationManager>(
    new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}