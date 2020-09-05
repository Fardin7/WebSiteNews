using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using Repository.Inerface;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Repository.Repository
{
   public class NewsSubCategoryRepository : GenericRepository<NewsSubCategory>, INewsSubCategoryRepository
    {
        private DbContext _context;
      // internal DbSet<Article> dbSet;
        public NewsSubCategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._context = unitOfWork.DBContext;

           


        }

        public NewsSubCategory GetByTitle(string title)
        {
            return _context.Set<NewsSubCategory>().Where(q => q.Title == title).FirstOrDefault();
        }

        public int GetCount()
        {
             
               var b=  dbSet.Count();
            return b;
               
        }

      
    }
}
