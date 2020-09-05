 namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makesomefeildrequerdable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "SubcategoryId", "dbo.Subcategory");
            DropForeignKey("dbo.News", "NewsSubcategoryId", "dbo.NewsSubCategories");
            DropIndex("dbo.News", new[] { "SubcategoryId" });
            DropIndex("dbo.News", new[] { "NewsSubcategoryId" });
            AlterColumn("dbo.News", "SubcategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.News", "NewsSubcategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.News", "SubcategoryId");
            CreateIndex("dbo.News", "NewsSubcategoryId");
            AddForeignKey("dbo.News", "SubcategoryId", "dbo.Subcategory", "Id", cascadeDelete: true);
            AddForeignKey("dbo.News", "NewsSubcategoryId", "dbo.NewsSubCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsSubcategoryId", "dbo.NewsSubCategories");
            DropForeignKey("dbo.News", "SubcategoryId", "dbo.Subcategory");
            DropIndex("dbo.News", new[] { "NewsSubcategoryId" });
            DropIndex("dbo.News", new[] { "SubcategoryId" });
            AlterColumn("dbo.News", "NewsSubcategoryId", c => c.Int());
            AlterColumn("dbo.News", "SubcategoryId", c => c.Int());
            CreateIndex("dbo.News", "NewsSubcategoryId");
            CreateIndex("dbo.News", "SubcategoryId");
            AddForeignKey("dbo.News", "NewsSubcategoryId", "dbo.NewsSubCategories", "Id");
            AddForeignKey("dbo.News", "SubcategoryId", "dbo.Subcategory", "Id");
        }
    }
}
