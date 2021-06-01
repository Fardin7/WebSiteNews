using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using Repository.Inerface;
using System.Data.Entity;

namespace Repository.Repository
{
    public class NewsCategoryRepository : GenericRepository<NewsCategory>, INewsCategoryRepository
    {
        private DbContext _context;
        // internal DbSet<Article> dbSet;
        public NewsCategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._context = unitOfWork.DBContext;




        }

        public NewsCategory GetByTitle(string title)
        {
            return _context.Set<NewsCategory>().Where(q => q.Title == title).FirstOrDefault();
        }

        public int GetCount()
        {

            var b = dbSet.Count();
            return b;

        }

        public IQueryable<IGrouping<string,News>> LastNewsOfNewsCategory( int newstype)
        {
            var newsofcategory = (from newscategory in dbSet
                                  join newssubcategory in _context.Set<NewsSubCategory>()
                                  on newscategory.Id equals newssubcategory.NewsCategoryId
                                  join news in _context.Set<News>()
                                  on newssubcategory.Id equals news.NewsSubcategoryId into categorysubcategorynews
                                  from csn in categorysubcategorynews
                                  where csn.NewsType==newstype && csn.IsActive
                                  orderby csn.PublishDate descending
                                  group csn by newscategory.Title
                                 );
                               ;
           

            return newsofcategory;
        }
    }
}
