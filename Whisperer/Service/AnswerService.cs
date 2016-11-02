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

        public AnswerService(IRepository<Answer> repository) : base(repository)
        {
            _meetingService = Ioc.Container.GetInstance<IMeetingService>();
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