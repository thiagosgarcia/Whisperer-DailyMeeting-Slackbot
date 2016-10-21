using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using Whisperer.DependencyResolution;
using Whisperer.Models;
using Whisperer.Service.Commands;
using ConfigurationModel = Whisperer.Models.Configuration;
namespace Whisperer.Service
{
    public class UserService 
    {
        private readonly Configuration _configuration;

        public UserService()
        {
            _configuration = Ioc.Container.GetInstance<Configuration>();
        }
        public async Task<UsersList> GetUsers()
        {
            using (var client = new HttpClient())
            {
                var parameters = new OutgoingUserParameters
                {
                    token = _configuration.GetAppToken(),
                    presence = 1
                };
                var content = new FormUrlEncodedContent(parameters.ToPairs());
                var response = await client.PostAsync("https://slack.com/api/users.list", content);
                return await response.Content.ReadAsAsync<UsersList>();
            }
        }
    }
}