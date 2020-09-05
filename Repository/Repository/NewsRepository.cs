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
   public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        private DbContext _context;
      // internal DbSet<Article> dbSet;
        public NewsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._context = unitOfWork.DBContext;

           


        }

        public News GetByTitleAndType(string title,int type)
        {
            return _context.Set<News>().Where(q => q.Title == title && q.NewsType==type).FirstOrDefault();
        }

        public int GetCount()
        {
             
               var b=  dbSet.Count();
            return b;
               
        }

        public List<News> ListNewsOfNewsCategoryAndCategory(int newstype, string categoryname, string newscategoryname,int count)
        {
            if (categoryname!=null && newscategoryname!=null)
            {
                return dbSet.Where((q => q.NewsSubCategory.NewsCategory.Title == newscategoryname && q.Subcategory.Title == categoryname && q.NewsType==newstype)).OrderByDescending(q=>q.Id).Take(count).ToList();
            }
            return dbSet.Where(q => q.NewsSubCategory.NewsCategory.Title == newscategoryname || q.Subcategory.Category.Title == categoryname ).Where(q=>q.NewsType == newstype).OrderByDescending(q => q.Id).Take(count).ToList();
        }

        public List<News> RelatedNewsPagin(int newstype, int categoryname, int newscategoryname, int count,int pagenumber)
        {
            var newsofallpages = count * pagenumber;
            var skipnews = (pagenumber - 1) * count;
            if (categoryname != 0 && newscategoryname != 0 && pagenumber>0)
            {
               
                var countall = dbSet.Where((q => q.NewsSubCategory.NewsCategory.Id == newscategoryname && q.Subcategory.Id == categoryname && q.NewsType == newstype)).Count();
                if (newsofallpages > countall)
                {

                    return dbSet.Where((q => q.NewsSubCategory.NewsCategory.Id == newscategoryname && q.Subcategory.Id == categoryname && q.NewsType == newstype)).OrderBy(q => q.Id).Take(count).ToList();

                   
                }
                else
                {
                    return dbSet.Where((q => q.NewsSubCategory.NewsCategory.Id == newscategoryname && q.Subcategory.Id == categoryname && q.NewsType == newstype)).OrderByDescending(q => q.Id).Take(newsofallpages).Skip(skipnews).ToList();

                }
               
               
            }
            else
            {
                return dbSet.Where((q => q.NewsSubCategory.NewsCategory.Id == newscategoryname || q.Subcategory.CategoryId == categoryname)).Where(q=>q.NewsType == newstype).OrderByDescending(q => q.Id).Take(newsofallpages).Skip(skipnews).ToList();

            }

        }
    }
}
