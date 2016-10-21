using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using Whisperer.DependencyResolution;
using Whisperer.Service;
using Whisperer.Service.Job;
using ConfigurationModel = Whisperer.Models.Configuration;

namespace Whisperer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Ioc.Initialize();
            UpdateConfiguration();
            StartJob();
        }

        private void UpdateConfiguration()
        {
            var service = Ioc.Container.GetInstance<IService<ConfigurationModel>>();
            var config = LoadConfigJson();

            config.Id = service.GetAll().OrderBy(x=> x.Id).First().Id;
            service.Update(config);
        }

        private ConfigurationModel LoadConfigJson()
        {
            using (var r = new StreamReader(Server.MapPath("~/App_Data/config.json")))
            {
                var json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<ConfigurationModel>(json);
            }
        }

        private async void StartJob()
        {
            Ioc.Container.GetInstance<DailyJob>().Start();
        }
    }
}
