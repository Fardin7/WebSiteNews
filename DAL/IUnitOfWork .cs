using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {

        DbContext DBContext { get; }
        DbContextTransaction BeginTransaction();
        int Complete();
        int Complete(bool usingTransaction);
        
    }
}
