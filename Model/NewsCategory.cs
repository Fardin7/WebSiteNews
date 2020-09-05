using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class NewsCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public virtual ICollection<NewsSubCategory> NewsSubCategories { get; set; }

    }
}
