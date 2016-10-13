using ConfigurationModel = Whisperer.Models.Configuration;
namespace Whisperer.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppContext context)
        {
            context.Configurations.AddOrUpdate(new ConfigurationModel
            {
                Id = 1
            });
        }
    }
}
