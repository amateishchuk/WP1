namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedQuestionModel_ChangedGuidToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "AskerId", c => c.String());
            AddColumn("dbo.Questions", "ResponderId", c => c.String());
            DropColumn("dbo.Questions", "Asker");
            DropColumn("dbo.Questions", "Responder");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Responder", c => c.Guid(nullable: false));
            AddColumn("dbo.Questions", "Asker", c => c.Guid());
            DropColumn("dbo.Questions", "ResponderId");
            DropColumn("dbo.Questions", "AskerId");
        }
    }
}
