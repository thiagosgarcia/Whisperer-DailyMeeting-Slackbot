using Whisperer.DependencyResolution;
using ConfigurationModel = Whisperer.Models.Configuration;

namespace Whisperer.Service
{
    public class Configuration
    {
        private IReadOnlyService<ConfigurationModel> _service;
        public ConfigurationModel Instance;

        public Configuration()
        {
            _service = Ioc.Container.GetInstance<IReadOnlyService<ConfigurationModel>>();
            LoadData();
        }

        public ConfigurationModel LoadData()
        {
            return Instance = _service.Get(1);
        }
    }
}