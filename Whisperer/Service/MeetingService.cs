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

            var meeting = _repository.Items.SingleOrDefault(x => x.Date == date);
            if (meeting != null)
                return meeting;

            return Add(new Meeting
            {
                Date = DateTime.Today
            });
        }
    }
}