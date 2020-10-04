using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Data.Entity;
using Model;
using Service.Interface;
using System.Web.Routing;

namespace Site.CustomAuthorization
{
    class CustomAuthorize : AuthorizeAttribute
    {

       // private readonly IArticleService _articleService;

        public CustomAuthorize()
        {

        }

        //public CustomAuthorize(/*DbContext context*/IArticleService articleService)
        //{
        //    //  _dbContext = context;
        //    _articleService = articleService;
        //}
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            
            var authorize = base.AuthorizeCore(httpContext);
            if (!authorize)
            {
                return authorize;
            }
            var unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();

            var userid = httpContext.User.Identity.GetUserId();
            var userpermmision = unitOfWork.DBContext.Set<ApplicationUser>().Where(q => q.Id == userid).FirstOrDefault().Permissions.ToList();
            List<Role> userrole = unitOfWork.DBContext.Database.SqlQuery<Role>("select *  from AspNetRoles R join  AspNetUserRoles UR ON R.Id = UR.RoleId JOIN  AspNetUsers U ON  U.Id = UR.UserId where U.Id={0}", userid).ToList();
            var rolepermmision = (from permission in unitOfWork.DBContext.Set<Permission>().ToList()
                                  join ur in userrole on permission.RoleId equals ur.Id
                                  select permission).ToList();
            var allpermission = (from permission in userpermmision
                                 select permission).Union(rolepermmision).ToList();

            var action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
            var controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var entity = (int)Enum.Parse(typeof(Entity), controller.ToLower());
            var function = (int)Enum.Parse(typeof(Action), action.ToLower());
            if (allpermission.Count>0)
            {
                foreach (var item in allpermission)
                {
                    if (item.Acrion == function && item.Entity == entity)
                    {
                        
                        authorize = true;
                        break;
                    }
                    else
                    {
                        authorize = false;
                    }
                }
            }
            else
            {
                authorize = false;
            }
     



            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary(
                   new
                   {
                      // Area = "admin",
                       controller = "Account",
                       action = "Login",
                       returnUrl="admin/"+ filterContext.RouteData.Values["controller"]+"/"+ filterContext.RouteData.Values["action"]
                   })
               );
                //base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        Area="admin",
                        controller = "Account",
                        action = "Unauthorised"
                    })
                );
            }


        }
    }
}
