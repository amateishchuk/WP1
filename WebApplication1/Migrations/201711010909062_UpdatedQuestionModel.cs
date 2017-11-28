namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedQuestionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "DateTimeAsk", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "DateTimeResponse", c => c.DateTime());
            DropColumn("dbo.Questions", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Questions", "DateTimeResponse");
            DropColumn("dbo.Questions", "DateTimeAsk");
        }
    }
}
