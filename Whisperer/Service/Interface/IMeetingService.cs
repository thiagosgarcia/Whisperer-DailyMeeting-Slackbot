using System;
using System.Threading.Tasks;
using Whisperer.Models;

namespace Whisperer.Service
{
    public interface IMeetingService : IService<Meeting>
    {
        Task<Meeting> GetOrAddMeeting(DateTime? date = default(DateTime?));
        Task<Meeting> GetByDate(DateTime? date = default(DateTime?));
    }
}