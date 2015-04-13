namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionSkills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuestionID = c.Int(nullable: false),
                        SkillID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillID, cascadeDelete: true)
                .Index(t => t.QuestionID)
                .Index(t => t.SkillID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionSkills", "SkillID", "dbo.Skills");
            DropForeignKey("dbo.QuestionSkills", "QuestionID", "dbo.Questions");
            DropIndex("dbo.QuestionSkills", new[] { "SkillID" });
            DropIndex("dbo.QuestionSkills", new[] { "QuestionID" });
            DropTable("dbo.QuestionSkills");
        }
    }
}
