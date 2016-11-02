namespace Whisperer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionStatuses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Active", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Active");
        }
    }
}
