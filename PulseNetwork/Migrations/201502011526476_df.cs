namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "tags");
        }
    }
}
