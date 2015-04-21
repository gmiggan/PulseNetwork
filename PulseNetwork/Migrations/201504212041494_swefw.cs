namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class swefw : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkspacePosts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        workspaceId = c.Int(nullable: false),
                        posterId = c.String(maxLength: 128),
                        TimePosted = c.DateTime(nullable: false),
                        postDetails = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.posterId)
                .ForeignKey("dbo.WorkspacePages", t => t.workspaceId, cascadeDelete: true)
                .Index(t => t.workspaceId)
                .Index(t => t.posterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkspacePosts", "workspaceId", "dbo.WorkspacePages");
            DropForeignKey("dbo.WorkspacePosts", "posterId", "dbo.AspNetUsers");
            DropIndex("dbo.WorkspacePosts", new[] { "posterId" });
            DropIndex("dbo.WorkspacePosts", new[] { "workspaceId" });
            DropTable("dbo.WorkspacePosts");
        }
    }
}
