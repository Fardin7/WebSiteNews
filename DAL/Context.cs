using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
   public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(/*string cs*/) : base("DefaultConnectionString")
        {
           // Article = new DbSet<Article>();
         

        }


        public static ApplicationDbContext Create()
        {
            
            return new ApplicationDbContext(/*"DefaultConnectionString"*/);
        }
        //public virtual DbSet<Article> Article { get; set; }
        //public virtual DbSet<ArticleCategory> ArticleCategory { get; set; }
        //public virtual DbSet<ArticleSubcategory> ArticleSubcategory { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Category> Categories{ get; set; }
        public virtual DbSet<Subcategory> Subcategories { get; set; }
        public virtual DbSet<NewsSubCategory> NewsSubCategories { get; set; }
        public virtual DbSet<NewsCategory> NewsCategories { get; set; }
        public virtual DbSet<NewsFile> NewsFiles { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<NewsLetter> NewsLetter { get; set; }

        ///public virtual DbSet<AspNetRole> AspNetRole { get; set; }
        // public virtual DbSet<AspNetUser> AspNetUser { get; set; }
        // public virtual DbSet<AspNetUserClaim> AspNetUserClaim  { get; set; }
        // public virtual DbSet<AspNetUserLogin> AspNetUserLogin  { get; set; }





    }
}
