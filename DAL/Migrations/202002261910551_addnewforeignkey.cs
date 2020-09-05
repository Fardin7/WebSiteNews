namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewforeignkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "NewsSubcategoryId", c => c.Int());
            CreateIndex("dbo.News", "NewsSubcategoryId");
            AddForeignKey("dbo.News", "NewsSubcategoryId", "dbo.NewsSubCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsSubcategoryId", "dbo.NewsSubCategories");
            DropIndex("dbo.News", new[] { "NewsSubcategoryId" });
            DropColumn("dbo.News", "NewsSubcategoryId");
        }
    }
}
