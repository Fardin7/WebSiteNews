namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfieldtonewsle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsLetters", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsLetters", "CreateDate");
        }
    }
}
