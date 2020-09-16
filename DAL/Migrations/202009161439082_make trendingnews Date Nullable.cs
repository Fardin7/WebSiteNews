namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maketrendingnewsDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "TrendingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "TrendingDate", c => c.DateTime(nullable: false));
        }
    }
}
