using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;
using Whisperer.App_Start;
using Whisperer.DependencyResolution;
using Whisperer.Models;
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

            Mapper.Initialize();

            UpdateConfiguration();
            UpdateQuestions();
            StartJob();
        }

        private void UpdateConfiguration()
        {
            var service = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IService<ConfigurationModel>>();
            var config = LoadConfigJson();

            config.Id = service.GetAll().OrderBy(x => x.Id).First().Id;
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

        private void UpdateQuestions()
        {
            var service = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IQuestionService>();
            var questions = LoadQuestionJson();

            (service.GetAll().Result).ToList().ForEach(x =>
            {
                x.Active = questions.Any(q => x.Text == q.Text);
                service.Update(x);
            });
            questions?.ForEach(x =>
            {
                var existing = service.GetByText(x.Text);
                if (existing == null)
                    service.Add(x);
                else
                {
                    existing.Text = x.Text;
                    existing.Active = x.Active;
                    service.Update(existing);
                }
            });
        }

        private List<Question> LoadQuestionJson()
        {
            using (var r = new StreamReader(Server.MapPath("~/App_Data/questions.json")))
            {
                var json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Question>>(json, new  JsonSerializerSettings() {Culture = CultureInfo.InvariantCulture});
            }
        }

        private async void StartJob()
        {
            StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<DailyJob>().Start();
        }
    }
}
