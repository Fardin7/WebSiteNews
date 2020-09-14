namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtrendingandbrandingfiledtonewsmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "IsTrend", c => c.Boolean(nullable: false));
            AddColumn("dbo.News", "IsBanner", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "IsBanner");
            DropColumn("dbo.News", "IsTrend");
        }
    }
}
