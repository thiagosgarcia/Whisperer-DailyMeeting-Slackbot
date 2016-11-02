using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebGrease.Css.Extensions;
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
        private IAnswerService _answerService;
        private IQuestionService _questionService;

        public DailyJob()
        {
            _configuration = Ioc.Container.GetInstance<Configuration>();
            _userService = Ioc.Container.GetInstance<IUserService>();
            _channelService = Ioc.Container.GetInstance<IChannelService>();
            _meetingService = Ioc.Container.GetInstance<IMeetingService>();
            _answerService = Ioc.Container.GetInstance<IAnswerService>();
            _questionService = Ioc.Container.GetInstance<IQuestionService>();
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
            var channel = await _channelService.GetChannelInfo();
            return channel?.channel;
        }

        public async Task<IEnumerable<ApiUser>> GetPendingUsersForChannel(IEnumerable<ApiUser> users, ApiChannel channel, Meeting meeting)
        {
            var list = new List<ApiUser>();

            var questions = (await _questionService.GetAll(true)).ToList();
            if(!questions.Any())
                return list;

            var answers = (await _answerService.GetByMeeting(meeting)).ToList();
            if (!answers.Any())
                return users;

            users.ForEach(u =>
            {
                if(answers.Count(x => x.User.UserId == u.id) < questions.Count)
                    list.Add(u);
            });
            //TODO ajustes depois que estiver monitorando respostas

            return list;
        }

        public async Task AskScrumQuestions(IEnumerable<ApiUser> users)
        {
            return;
        }
    }
}