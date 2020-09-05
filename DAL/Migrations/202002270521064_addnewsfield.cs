namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewsfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "NewsType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "NewsType");
        }
    }
}
