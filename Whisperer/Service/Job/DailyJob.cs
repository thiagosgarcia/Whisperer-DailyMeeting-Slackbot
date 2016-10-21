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
        private UserService _userService;

        public DailyJob()
        {
            _configuration = Ioc.Container.GetInstance<Configuration>();
            _userService = Ioc.Container.GetInstance<UserService>();
        }
        public async void Start()
        {
            while (true)
            {
                try
                {
                    var users = await GetActiveUsers();
                    var channel = await GetChannelInfo();
                    users = await GetPendingUsersForChannel(users, channel);
                    await AskScrumQuestions(users);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Thread.Sleep(1000 * 60 * 5);
                }
            }
        }

        public async Task<Channel> GetChannelInfo()
        {
            return null;
        }

        public async Task<IEnumerable<ApiUser>> GetActiveUsers()
        {
            var u = await _userService.GetUsers();
            return u.members.Where(x => x.presence == "active");
        }

        public async Task<IEnumerable<ApiUser>> GetPendingUsersForChannel(IEnumerable<ApiUser> users, Channel channel)
        {
            throw new NotImplementedException();
        }

        public async Task AskScrumQuestions(IEnumerable<ApiUser> users)
        {
            return;
        }
    }
}