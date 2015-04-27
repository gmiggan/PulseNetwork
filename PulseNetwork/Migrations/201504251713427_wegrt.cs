namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wegrt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WorkspacePosts", "TimePosted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkspacePosts", "TimePosted", c => c.DateTime(nullable: false));
        }
    }
}
