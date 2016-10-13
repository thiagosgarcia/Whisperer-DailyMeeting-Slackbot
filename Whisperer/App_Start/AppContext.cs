using System.Data.Entity;
using Whisperer.Models;

namespace Whisperer
{
    public class AppContext : DbContext
    {
#if DEBUG
        public AppContext() : base("DefaultConnection")
        {
        }
#else
        public AppContext() : base("ProductionConnection")
        {
        }
#endif

        public IDbSet<Answer> Answers { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Team> Teams { get; set; }
        public IDbSet<Channel> Channels { get; set; }
        public IDbSet<Question> Questions { get; set; }
        public IDbSet<Meeting> Meetings { get; set; }
        public IDbSet<Configuration> Configurations { get; set; }
    }
}