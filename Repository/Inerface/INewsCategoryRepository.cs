using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace Repository.Inerface
{
  public  interface INewsCategoryRepository : IRepository<NewsCategory>
    {

        IQueryable<IGrouping<string, News>> LastNewsOfNewsCategory(int newstype);
        NewsCategory GetByTitle(string title);
    }
}
