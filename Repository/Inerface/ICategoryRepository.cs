﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace Repository.Inerface
{
  public  interface ICategoryRepository:IRepository<Category>
    {
        IQueryable<IGrouping<string, News>> LastNewsOfCategory(int newstype);
        Category GetByTitle(string title);
    }
}
