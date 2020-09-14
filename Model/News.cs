namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Body { get; set; }
        [Required]
        public int NewsType { get; set; }

        [StringLength(500)]
        public string KeyWord { get; set; }

        public DateTime? PublishDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsTrend { get; set; }

        public DateTime TrendingDate { get; set; }
        public bool IsBanner { get; set; }

        [StringLength(500)]
        public string ImageAddress { get; set; }
        [Required]
        public int SubcategoryId { get; set; }

        public virtual Subcategory Subcategory { get; set; }
        [Required]
        public int NewsSubcategoryId { get; set; }
        public virtual NewsSubCategory NewsSubCategory { get; set; }
        public virtual ICollection<NewsFile> NewsFiles { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
