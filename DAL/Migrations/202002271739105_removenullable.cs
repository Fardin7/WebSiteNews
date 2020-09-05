namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "IsActive", c => c.Boolean());
        }
    }
}
