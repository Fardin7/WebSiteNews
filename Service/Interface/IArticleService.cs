﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Service.Interface
{
  public  interface IArticleService:Iservice<Article>
    {
        int GetCount();
        Article GetByTitle(string title);
    }
}
