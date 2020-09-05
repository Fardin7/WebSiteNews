namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newforiegnkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.News", "ApplicationUserId");
            AddForeignKey("dbo.News", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.News", new[] { "ApplicationUserId" });
            DropColumn("dbo.News", "ApplicationUserId");
        }
    }
}
