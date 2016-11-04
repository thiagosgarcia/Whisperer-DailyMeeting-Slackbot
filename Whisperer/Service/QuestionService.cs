using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Policy;
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
    public class QuestionService : BaseService<Question>, IQuestionService
    {
        private readonly Configuration _configuration;

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
            using (var client = new HttpClient())
            {
                var parameters = new OutgoingDirectMessageOpenParameters
                {
                    token = _configuration.Instance.AppToken,
                    user = user.id
                };

                var content = new FormUrlEncodedContent(parameters.ToPairs());
                var response = await client.PostAsync("https://slack.com/api/im.open", content);
                var channelInfo = await response.Content.ReadAsAsync<DirectMessageOpenResponse>();

                //TODO Ajustar e continuar aqui
                var p2 = new OutgoingDirectMessageParameters
                {
                    token = _configuration.Instance.AppToken,
                    channel = channelInfo.channel.id,
                    text = question.Text
                };
                var r = await client.GetAsync(p2.GetUrl());
                var s = await r.Content.ReadAsAsync<DirectMessageResponse>();

                return s;
            }

        }
    }
}