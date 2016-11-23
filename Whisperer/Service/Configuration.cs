using Whisperer.DependencyResolution;
using ConfigurationModel = Whisperer.Models.Configuration;

namespace Whisperer.Service
{
    public class Configuration
    {
        private IReadOnlyService<ConfigurationModel> _service;
        public ConfigurationModel Instance;

        public Configuration(IReadOnlyService<ConfigurationModel> service)
        {
            _service = service;
            LoadData();
        }

        public ConfigurationModel LoadData()
        {
            return Instance = _service.Get(1);
        }
    }
}