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
   public class SubCategoryRepository : GenericRepository<Subcategory>, ISubCategoryRepository
    {
        private DbContext _context;
        public SubCategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._context = unitOfWork.DBContext;
        }

        public Subcategory GetByTitle(string title)
        {
            return _context.Set<Subcategory>().Where(q => q.Title == title).FirstOrDefault();
        }

        public int GetCount()
        {
             
               var b=  dbSet.Count();
            return b;
               
        }

      
    }
}
