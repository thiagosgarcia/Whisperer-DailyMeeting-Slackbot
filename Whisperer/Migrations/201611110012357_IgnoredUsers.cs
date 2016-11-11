namespace Whisperer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IgnoredUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Configurations", "IgnoredUsers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Configurations", "IgnoredUsers");
        }
    }
}
