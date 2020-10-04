namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjdsnksd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Description", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Description", c => c.String(nullable: false));
        }
    }
}
