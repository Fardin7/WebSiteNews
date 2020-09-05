﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace Repository.Inerface
{
  public  interface INewsRepository:IRepository<News>
    {

        int GetCount();
        News GetByTitleAndType(string title,int type);
        List<News> ListNewsOfNewsCategoryAndCategory(int newstype, string categoryname, string newscategoryname, int count);
        List<News> RelatedNewsPagin(int newstype, int categoryname, int newscategoryname, int count, int pagenumber);

    }
}