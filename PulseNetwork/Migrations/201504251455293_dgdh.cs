namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dgdh : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.WorkspacePosts", "workspaceId", "dbo.Workspaces", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkspacePosts", "workspaceId", "dbo.Workspaces");
        }
    }
}
