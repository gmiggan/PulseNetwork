namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SKillUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "Question_ID", c => c.Int());
            CreateIndex("dbo.Skills", "Question_ID");
            AddForeignKey("dbo.Skills", "Question_ID", "dbo.Questions", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "Question_ID", "dbo.Questions");
            DropIndex("dbo.Skills", new[] { "Question_ID" });
            DropColumn("dbo.Skills", "Question_ID");
        }
    }
}
