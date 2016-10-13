namespace Whisperer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        TriggerWord = c.String(),
                        Channel_Id = c.Long(),
                        Meeting_Id = c.Long(),
                        Question_Id = c.Long(),
                        Team_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Channels", t => t.Channel_Id)
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Channel_Id)
                .Index(t => t.Meeting_Id)
                .Index(t => t.Question_Id)
                .Index(t => t.Team_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChannelId = c.String(),
                        ChannelName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TeamId = c.String(),
                        TeamDomain = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OfficeHoursBegin = c.Short(nullable: false),
                        OfficeHoursEnd = c.Short(nullable: false),
                        DailyMeetingTime = c.Short(nullable: false),
                        IncomingToken = c.String(),
                        OutgoingToken = c.String(),
                        PayloadUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Answers", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Answers", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.Answers", "Channel_Id", "dbo.Channels");
            DropIndex("dbo.Answers", new[] { "User_Id" });
            DropIndex("dbo.Answers", new[] { "Team_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Answers", new[] { "Meeting_Id" });
            DropIndex("dbo.Answers", new[] { "Channel_Id" });
            DropTable("dbo.Configurations");
            DropTable("dbo.Users");
            DropTable("dbo.Teams");
            DropTable("dbo.Questions");
            DropTable("dbo.Meetings");
            DropTable("dbo.Channels");
            DropTable("dbo.Answers");
        }
    }
}
