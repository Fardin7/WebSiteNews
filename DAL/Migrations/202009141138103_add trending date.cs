namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtrendingdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "TrendingDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "TrendingDate");
        }
    }
}
