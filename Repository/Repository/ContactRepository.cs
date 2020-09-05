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
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private DbContext _context;
        // internal DbSet<Article> dbSet;
        public ContactRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._context = unitOfWork.DBContext;




        }


    }
}
