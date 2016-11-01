namespace Whisperer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultChannel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Configurations", "DefaultChannel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Configurations", "DefaultChannel");
        }
    }
}