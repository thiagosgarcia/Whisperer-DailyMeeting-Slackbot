namespace Whisperer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppTokenSupport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Configurations", "AppToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Configurations", "AppToken");
        }
    }
}
