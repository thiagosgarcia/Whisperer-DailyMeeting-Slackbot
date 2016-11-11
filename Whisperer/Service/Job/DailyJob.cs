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
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Thread.Sleep(_configuration.Instance.GetAnswerTimeout());
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

            List<Question> questions = null;
            List<Answer> answers = null;

            var loadQuestionsTask = Task.Run(async () => questions = (await _questionService.GetAll(true)).ToList());
            var loadAnswersTask = Task.Run(async () => answers = (await _answerService.GetByMeeting(meeting)).ToList());

            await Task.WhenAll(loadAnswersTask, loadQuestionsTask);

            Parallel.ForEach(users, u =>
            {
                if(_configuration.Instance.IgnoredUsersList.Contains(u.name, StringComparer.InvariantCultureIgnoreCase))
                    return;
                var userAnswers = answers.Where(x => x.User.UserId == u.id).ToList();
                if (userAnswers.Count() == questions.Count)
                    return;

                var questionsLeft = GetQuestionsLeft(userAnswers, questions);
                if (!questionsLeft.Any()) //TODO Verificar se realmente preciso disso
                    return;

                Task.Run(() => AskScrumQuestion(u, questionsLeft.FirstOrDefault()));
                list.Add(u);
            });
            //TODO ajustes depois que estiver monitorando respostas

            return list;
        }

        private List<Question> GetQuestionsLeft(List<Answer> userAnswers, List<Question> questions)
        {
            var questionsLeft = new List<Question>();
            questions.ForEach(q =>
            {
                if(userAnswers.All(x => x.Question.Id != q.Id))
                    questionsLeft.Add(q);
            });
            return questionsLeft;
        }

        public async Task AskScrumQuestion(ApiUser user, Question question)
        {
            var timeout = DateTime.Now.AddMinutes(_configuration.Instance.GetAnswerTimeout());
            var response = await _questionService.Ask(user, question);
            var answer = await _questionService.TrackAnswer(timeout, response, user);

            if (answer == null)
                return;
            //TODO Persistir resposta

            //TODO Monitorar resposta do usuário
            //TODO Tratar response, adicionar logs, etc
        }
    }
}