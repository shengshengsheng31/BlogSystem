namespace BlogSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20191223 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Content", c => c.String(nullable: false, maxLength: 140));
            DropColumn("dbo.Comments", "Context");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Context", c => c.String(nullable: false, maxLength: 140));
            DropColumn("dbo.Comments", "Content");
        }
    }
}
