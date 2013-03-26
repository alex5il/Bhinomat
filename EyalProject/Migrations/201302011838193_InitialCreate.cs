namespace EyalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        LastUseDate = c.DateTime(nullable: false),
                        Subject = c.String(),
                        SecondarySubject = c.String(),
                        Difficulty = c.String(),
                    })
                .PrimaryKey(t => t.QuestionId);
            
            CreateTable(
                "dbo.TrueFalseQuestions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        Question = c.String(),
                        AnswearOne = c.String(),
                        AnswearTwo = c.String(),
                        AnswearCorrect = c.String(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TrueFalseQuestions", new[] { "QuestionId" });
            DropForeignKey("dbo.TrueFalseQuestions", "QuestionId", "dbo.Questions");
            DropTable("dbo.TrueFalseQuestions");
            DropTable("dbo.Questions");
        }
    }
}
