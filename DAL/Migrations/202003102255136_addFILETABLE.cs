namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFILETABLE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Size = c.Int(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsFiles", "NewsId", "dbo.News");
            DropIndex("dbo.NewsFiles", new[] { "NewsId" });
            DropTable("dbo.NewsFiles");
        }
    }
}
