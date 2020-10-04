namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjkj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.News", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
