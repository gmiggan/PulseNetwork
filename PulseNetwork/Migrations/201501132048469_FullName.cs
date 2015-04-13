namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Picture", c => c.String());
            AddColumn("dbo.AspNetUsers", "ExperiencePoints", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ExperiencePoints");
            DropColumn("dbo.AspNetUsers", "Picture");
            DropColumn("dbo.AspNetUsers", "FullName");
        }
    }
}
