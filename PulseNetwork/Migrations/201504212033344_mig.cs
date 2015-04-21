namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkspacePages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        workspaceid = c.Int(nullable: false),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .ForeignKey("dbo.Workspaces", t => t.workspaceid, cascadeDelete: true)
                .Index(t => t.workspaceid)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkspacePages", "workspaceid", "dbo.Workspaces");
            DropForeignKey("dbo.WorkspacePages", "userId", "dbo.AspNetUsers");
            DropIndex("dbo.WorkspacePages", new[] { "userId" });
            DropIndex("dbo.WorkspacePages", new[] { "workspaceid" });
            DropTable("dbo.WorkspacePages");
        }
    }
}
