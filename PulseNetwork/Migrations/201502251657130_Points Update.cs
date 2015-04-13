namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PointsUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "points", c => c.Double(nullable: false));
            AddColumn("dbo.Questions", "Points", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Points");
            DropColumn("dbo.Answers", "points");
        }
    }
}
