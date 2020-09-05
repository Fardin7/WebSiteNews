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
   public class NewsFileRepository : GenericRepository<NewsFile>, INewsFileRepository
    {
        private DbContext _context;
      // internal DbSet<Article> dbSet;
        public NewsFileRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._context = unitOfWork.DBContext;

           


        }


      
    }
}
