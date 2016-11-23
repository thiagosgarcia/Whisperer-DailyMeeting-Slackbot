using System;
using System.Net.Http;
using System.Threading.Tasks;
using Whisperer.DependencyResolution;
using Whisperer.Models;
using Whisperer.Service.Commands;
using ConfigurationModel = Whisperer.Models.Configuration;
namespace Whisperer.Service
{
    public class PostBackService : IPostBackService
    {
        private readonly Configuration _configuration;
        private readonly MatchCommand _matchCommand;

        public PostBackService(Configuration configuration, MatchCommand matchCommand)
        {
            _configuration = configuration;
            _matchCommand = matchCommand;
        }
        public async Task<string> Ping()
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(_configuration.Instance.PayloadUrl, Pong());
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<CustomOutgoingPostData> Command(IncomingPostData data)
        {
            if (data.token != _configuration.Instance.IncomingToken)
                throw new Exception();

            return await _matchCommand.TryMatch(data.text)?.Action(data);
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