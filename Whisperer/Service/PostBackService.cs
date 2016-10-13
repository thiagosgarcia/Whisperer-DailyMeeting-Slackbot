using System;
using System.Net.Http;
using System.Threading.Tasks;
using Whisperer.DependencyResolution;
using Whisperer.Models;

using ConfigurationModel = Whisperer.Models.Configuration;
namespace Whisperer.Service
{
    public class PostBackService : IPostBackService
    {
        private readonly Configuration _configuration;

        public PostBackService()
        {
            _configuration = Ioc.Container.GetInstance<Configuration>();
        }
        public async Task<string> Ping()
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(_configuration.GetPayloadUrl(), Pong());
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<CustomOutgoingPostData> Command(IncomingPostData data)
        {
            if (data.token != _configuration.GetIncomingToken())
                throw new Exception();

            if (data.text.StartsWith("ping"))
            {
                return Pong();
            }

            return null;
        }

        private CustomOutgoingPostData Pong()
        {
            return new CustomOutgoingPostData
            {
                text = "Pong!",
                icon_emoji = ":table_tennis_paddle_and_ball:",
                username = "Whisperer Bot"
            };
    }
    }
}