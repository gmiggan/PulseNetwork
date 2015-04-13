namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Count : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Count");
            DropColumn("dbo.Answers", "Count");
        }
    }
}
