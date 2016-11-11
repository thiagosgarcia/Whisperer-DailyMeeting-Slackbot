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
    public class AnswerService : BaseService<Answer>, IAnswerService
    {
        private IMeetingService _meetingService;
        private IQuestionService _questionService;
        private IUserService _userService;

        public AnswerService(IRepository<Answer> repository) : base(repository)
        {
            _meetingService = Ioc.Container.GetInstance<IMeetingService>();
            _questionService = Ioc.Container.GetInstance<IQuestionService>();
            _userService = Ioc.Container.GetInstance<IUserService>();

            BeforeAdd += BeforeAddOrUpdate;
            BeforeUpdate += BeforeAddOrUpdate;
        }

        private void BeforeAddOrUpdate(Answer x)
        {
            x.Question = x.Question != null ? _questionService.Get(x.Question.Id) : null;
            x.Meeting = x.Meeting != null ? _meetingService.Get(x.Meeting.Id) : null;
            x.User = x.User != null ? _userService.GetByUserId(x.User.UserId) : null;
        }

        public async Task<IEnumerable<Answer>> GetByMeeting(DateTime? date = null)
        {
            return await GetByMeeting(await _meetingService.GetByDate(date));
        }
        public async Task<IEnumerable<Answer>> GetByMeeting(Meeting meeting)
        {
            return _repository.Items.Where(x => x.Meeting.Id == meeting.Id);
        }
    }
}