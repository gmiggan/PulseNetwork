namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kgh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkspacePosts", "workspaceId", "dbo.WorkspaceInvites");
        }
        
        public override void Down()
        {
            AddForeignKey("dbo.WorkspacePosts", "workspaceId", "dbo.WorkspaceInvites", "id", cascadeDelete: true);
        }
    }
}
