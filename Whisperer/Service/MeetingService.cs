using System;
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
    public class MeetingService : BaseService<Meeting>, IMeetingService
    {

        public MeetingService(IRepository<Meeting> repository) : base(repository)
        {
        }

        public async Task<Meeting> GetOrAddMeeting(DateTime? date = null)
        {
            if (date == null)
                date = DateTime.Today;

            var meeting = await GetByDate(date);
            if (meeting != null)
                return meeting;

            return Add(new Meeting
            {
                Date = DateTime.Today
            });
        }

        public async Task<Meeting> GetByDate(DateTime? date = null)
        {
            var tomorrow = date?.AddDays(1);
            return _repository.Items.SingleOrDefault(x => x.Date >= date && x.Date < tomorrow.Value);
        }
    }
}