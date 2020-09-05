namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjhk : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subcategory", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subcategory", "IsActive", c => c.Boolean());
        }
    }
}
