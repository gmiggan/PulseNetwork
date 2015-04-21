namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mii : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workspaces",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        creatorID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.creatorID)
                .Index(t => t.creatorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workspaces", "creatorID", "dbo.AspNetUsers");
            DropIndex("dbo.Workspaces", new[] { "creatorID" });
            DropTable("dbo.Workspaces");
        }
    }
}
