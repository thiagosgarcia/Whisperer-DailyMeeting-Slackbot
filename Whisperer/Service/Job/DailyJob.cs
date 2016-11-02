using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Whisperer.DependencyResolution;
using Whisperer.Models;

namespace Whisperer.Service.Job
{
    public class DailyJob
    {
        private Configuration _configuration;
        private IUserService _userService;
        private IChannelService _channelService;
        private IMeetingService _meetingService;

        public DailyJob()
        {
            _configuration = Ioc.Container.GetInstance<Configuration>();
            _userService = Ioc.Container.GetInstance<IUserService>();
            _channelService = Ioc.Container.GetInstance<IChannelService>();
            _meetingService = Ioc.Container.GetInstance<IMeetingService>();
        }
        public async void Start()
        {
            while (true)
            {
                try
                {
                    var users = await GetActiveUsers();
                    var channel = await GetDefaultChannelInfo();
                    var meeting = await _meetingService.GetOrAddMeeting();
                    users = await GetPendingUsersForChannel(users, channel, meeting);
                    await AskScrumQuestions(users);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Thread.Sleep(1000 /* * 60*/ * 5);
                }
            }
        }

        public async Task<IEnumerable<ApiUser>> GetActiveUsers()
        {
            var u = await _userService.GetUsers();
            return u.members.Where(x => x.presence == "active");
        }

        public async Task<ApiChannel> GetDefaultChannelInfo()
        {
            var channel =  await _channelService.GetChannelInfo();
            return channel?.channel;
        }

        public async Task<IEnumerable<ApiUser>> GetPendingUsersForChannel(IEnumerable<ApiUser> users, ApiChannel channel, Meeting meeting)
        {
            //TODO continuar aqui
            return null;
        }

        public async Task AskScrumQuestions(IEnumerable<ApiUser> users)
        {
            return;
        }
    }
}