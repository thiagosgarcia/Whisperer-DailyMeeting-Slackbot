using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using MegaStore.Persistence;
using WebGrease.Css.Extensions;
using Whisperer.App_Start;
using Whisperer.DependencyResolution;
using Whisperer.Models;
using Whisperer.Service.Commands;
using ConfigurationModel = Whisperer.Models.Configuration;
namespace Whisperer.Service
{
    public class ChannelService : BaseService<Channel>, IChannelService
    {
        private readonly Configuration _configuration;
        private IService<Channel> _userService;

        public ChannelService(IRepository<Channel> repository) : base(repository)
        {
            _configuration = Ioc.Container.GetInstance<Configuration>();
        }

        public async Task<ChannelsList> GetChannelList()
        {

            using (var client = new HttpClient())
            {
                var parameters = new OutgoingChannelListParameters
                {
                    token = _configuration.Instance.AppToken
                };
                var content = new FormUrlEncodedContent(parameters.ToPairs());
                var response = await client.PostAsync("https://slack.com/api/channels.list", content);
                var channels = await response.Content.ReadAsAsync<ChannelsList>();

                await AddNewChannels(channels);

                return channels;
            }

        }
        private async Task<int> AddNewChannels(ChannelsList channels)
        {
            var count = 0;
            channels.channels.ForEach(x =>
            {
                var channel = GetByChannelId(x.id);
                if (channel != null)
                    return;

                Add(Mapper.Map.Map<ApiChannel, Channel>(x));
                count++;
            });
            return count;
        }

        private Channel GetByChannelId(string channelId)
        {
            return _repository.Items.FirstOrDefault(x => x.ChannelId == channelId);
        }

        public async Task<ChannelInfo> GetChannelInfo()
        {
            //https://slack.com/api/channels.info

            using (var client = new HttpClient())
            {
                var channelList = await GetChannelList();

                var parameters = new OutgoingChannelInfoParameters
                {
                    token = _configuration.Instance.AppToken,
                    channel = channelList.channels.FirstOrDefault(x=> x.name == _configuration.Instance.DefaultChannel)?.id
                };
                var content = new FormUrlEncodedContent(parameters.ToPairs());
                var response = await client.PostAsync("https://slack.com/api/channels.info", content);
                var channelInfo = await response.Content.ReadAsAsync<ChannelInfo>();

                return channelInfo;
            }
        }

    }
}