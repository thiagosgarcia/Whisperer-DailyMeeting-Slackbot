using System;
using System.Threading.Tasks;
using Whisperer.Models;

namespace Whisperer.Service
{
    public interface IMeetingService
    {
        Task<Meeting> GetOrAddMeeting(DateTime? date = default(DateTime?));
    }
}