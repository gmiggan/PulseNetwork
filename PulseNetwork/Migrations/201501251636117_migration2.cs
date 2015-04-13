namespace PulseNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        QuestionID = c.Int(nullable: false),
                        AnswerBody = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        JobTitle = c.String(),
                        Company = c.String(),
                        Location = c.String(),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        skillName = c.String(),
                        skillDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserSkills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        skillID = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.Skills", t => t.skillID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.skillID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSkills", "skillID", "dbo.Skills");
            DropForeignKey("dbo.UserSkills", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Profiles", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Answers", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.UserSkills", new[] { "skillID" });
            DropIndex("dbo.UserSkills", new[] { "UserID" });
            DropIndex("dbo.Profiles", new[] { "UserID" });
            DropIndex("dbo.Answers", new[] { "QuestionID" });
            DropIndex("dbo.Answers", new[] { "UserID" });
            DropTable("dbo.UserSkills");
            DropTable("dbo.Skills");
            DropTable("dbo.Profiles");
            DropTable("dbo.Answers");
        }
    }
}
