namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sadfasdfsa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.News", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Description", c => c.String());
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
