namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjn : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.News", new[] { "NewsSubCategoryId" });
            CreateIndex("dbo.News", "NewsSubcategoryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.News", new[] { "NewsSubcategoryId" });
            CreateIndex("dbo.News", "NewsSubCategoryId");
        }
    }
}
