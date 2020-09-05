namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullabledatefield : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsSubCategories", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.NewsCategories", "CreateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsCategories", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.NewsSubCategories", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
