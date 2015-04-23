namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hello : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WorkspacePages", newName: "WorkspaceInvites");
            AddColumn("dbo.AspNetUsers", "Workspace_id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Workspace_id");
            AddForeignKey("dbo.AspNetUsers", "Workspace_id", "dbo.Workspaces", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Workspace_id", "dbo.Workspaces");
            DropIndex("dbo.AspNetUsers", new[] { "Workspace_id" });
            DropColumn("dbo.AspNetUsers", "Workspace_id");
            RenameTable(name: "dbo.WorkspaceInvites", newName: "WorkspacePages");
        }
    }
}
