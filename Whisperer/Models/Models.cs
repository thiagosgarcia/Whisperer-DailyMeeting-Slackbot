using System;

namespace Whisperer.Models
{
    public class Configuration : Entity
    {
        public short OfficeHoursBegin { get; set; }
        public short OfficeHoursEnd { get; set; }
        public short DailyMeetingTime { get; set; }
        public string IncomingToken { get; set; }
        public string OutgoingToken { get; set; }
        public string AppToken { get; set; }
        public string DefaultChannel { get; set; }
        public string PayloadUrl { get; set; }
        public string Language { get; set; }
    }

    public class Entity
    {
        public long Id { get; set; }
    }

    public class Meeting : Entity
    {
        public DateTime Date { get; set; }
    }

    public class Question : Entity
    {
        public string Text { get; set; }
        public bool Active { get; set; }
    }

    public class Team : Entity
    {
        public string TeamId { get; set; }
        public string TeamDomain { get; set; }
    }

    public class Channel : Entity
    {
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
    }

    public class User : Entity
    {
        public string UserId { get; set; }
        public string Username { get; set; }
    }

    public class Answer : Entity
    {
        public string Text { get; set; }
        public string TriggerWord { get; set; }
        public virtual User User { get; set; }
        public virtual Team Team { get; set; }
        public virtual Channel Channel { get; set; }
        public virtual Question Question { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}