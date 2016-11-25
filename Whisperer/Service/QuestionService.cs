using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using log4net;
using MegaStore.Persistence;
using WebGrease.Css.Extensions;
using Whisperer.App_Start;
using Whisperer.DependencyResolution;
using Whisperer.Models;
using Whisperer.Service.Commands;
using Whisperer.Service.Job;
using ConfigurationModel = Whisperer.Models.Configuration;
namespace Whisperer.Service
{
    public class QuestionService : BaseService<Question>, IQuestionService
    {
        private readonly Configuration _configuration;
        private static readonly ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public QuestionService(IRepository<Question> repository, Configuration configuration) : base(repository)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Question>> GetAll(bool? active)
        {
            if (!active.HasValue)
                return GetAll();

            return _repository.Items.Where(x => x.Active == active.Value);
        }

        public Question GetByText(string text)
        {
            return _repository.Items.FirstOrDefault(x => x.Text == text);
        }

        public async Task<DirectMessageResponse> Ask(ApiUser user, Question question)
        {
            var channelInfo = await OpenDirectMessage(user);
            var questionAnser = await DoAskQuestion(channelInfo, question);

            return questionAnser;
        }

        public async Task<DirectMessageHistoryResponse> TrackAnswer(DateTime timeout, DirectMessageResponse messageResponse, ApiUser user)
        {
            while (timeout.CompareTo(DateTime.Now) > 0)
            {
                using (var client = new HttpClient())
                {
                    Log.Info($"Looking for answers from {user.name} since {messageResponse.ts}");
                    var parameters = new OutgoingDirectMessageHistoryParameters
                    {
                        token = _configuration.Instance.AppToken,
                        channel = messageResponse.channel,
                        oldest = messageResponse.ts,
                        inclusive = 0
                    };
                    var response = await client.GetAsync(parameters.GetUrl());
                    var messages = await response.Content.ReadAsAsync<DirectMessageHistoryResponse>();

                    if (messages.messages == null || messages.messages.Length == 0)
                    {
                        Log.Info($"No answers from {user.name}, waiting...");
                        await Task.Delay(5000);
                        continue;
                    }

                    return messages;
                }
            }

            return null;
        }

        private async Task<DirectMessageResponse> DoAskQuestion(
            DirectMessageOpenResponse channelInfo, Question question)
        {
            using (var client = new HttpClient())
            {
                var parameters = new OutgoingDirectMessageParameters
                {
                    token = _configuration.Instance.AppToken,
                    channel = channelInfo.channel.id,
                    text = question.Text
                };
                var response = await client.GetAsync(parameters.GetUrl());
                return await response.Content.ReadAsAsync<DirectMessageResponse>();
            }
        }

        private async Task<DirectMessageOpenResponse> OpenDirectMessage(ApiUser user)
        {
            using (var client = new HttpClient())
            {
                var parameters = new OutgoingDirectMessageOpenParameters
                {
                    token = _configuration.Instance.AppToken,
                    user = user.id
                };

                var content = new FormUrlEncodedContent(parameters.ToPairs());
                var response = await client.PostAsync("https://slack.com/api/im.open", content);
                return await response.Content.ReadAsAsync<DirectMessageOpenResponse>();
            }
        }
    }
}