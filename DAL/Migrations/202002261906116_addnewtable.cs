namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsSubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        NewsCategoryId = c.Int(),
                        NewsSubCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsCategories", t => t.NewsCategoryId)
                .ForeignKey("dbo.NewsSubCategories", t => t.NewsSubCategoryId)
                .Index(t => t.NewsCategoryId)
                .Index(t => t.NewsSubCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsSubCategories", "NewsSubCategoryId", "dbo.NewsSubCategories");
            DropForeignKey("dbo.NewsSubCategories", "NewsCategoryId", "dbo.NewsCategories");
            DropIndex("dbo.NewsSubCategories", new[] { "NewsSubCategoryId" });
            DropIndex("dbo.NewsSubCategories", new[] { "NewsCategoryId" });
            DropTable("dbo.NewsSubCategories");
            DropTable("dbo.NewsCategories");
        }
    }
}
