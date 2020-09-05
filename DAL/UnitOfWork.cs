using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext dbcontext = null;
        public UnitOfWork(/*string ConnectionString*/DbContext dbContext)
        {
            if (dbcontext==null)
            {
                dbcontext = dbContext;
                    ///new ApplicationDbContext(ConnectionString);

            }

        }


        public DbContext DBContext
        {
            get
            {
                return dbcontext;
            }
        }


        public DbContextTransaction BeginTransaction()
        {
            return dbcontext.Database.BeginTransaction();
        }
        public int EndTransaction(DbContextTransaction transaction)
        {
            int status = -1;
            try
            {
                status = dbcontext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            return status;
        }

        public int Complete()
        {
            return dbcontext.SaveChanges();
        }

        public int Complete(bool usingTransaction)
        {
            if (!usingTransaction)
                return Complete();

            int status = -1;
            using (var ts = dbcontext.Database.BeginTransaction())
            {
                try
                {
                    status = dbcontext.SaveChanges();
                    ts.Commit();
                }
                catch (Exception)
                {
                    ts.Rollback();
                    throw;
                }
            }
            return status;
        }

        public void Dispose()
        {
            if (dbcontext != null)
                dbcontext.Dispose();
        }

        //public IUnitOfWork New()
        //{
        //    dbcontext = null;
        //    // TryCreateContext();
        //    return this;
        //}

        //void TryCreateContext()
        //{
        //    if (ContextType == default(Type))
        //        throw new Exception("Context type Unknown");

        //    if (_context == null)
        //        _context = Activator.CreateInstance(ContextType) as DbContext;
        //}
    }
}
