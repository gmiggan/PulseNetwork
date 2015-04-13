namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relationship1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Questions", "UserID");
            AddForeignKey("dbo.Questions", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Questions", new[] { "UserID" });
            AlterColumn("dbo.Questions", "UserID", c => c.String());
        }
    }
}
