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
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private DbContext _context;
        public CommentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._context = unitOfWork.DBContext;
        }


    }
}
