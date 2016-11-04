namespace Whisperer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnswerTimeoutConfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Configurations", "AnswerTimeout", c => c.Int(nullable: false, defaultValue: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Configurations", "AnswerTimeout");
        }
    }
}
