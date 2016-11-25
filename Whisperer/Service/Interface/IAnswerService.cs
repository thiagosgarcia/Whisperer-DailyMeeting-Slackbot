using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Whisperer.Models;

namespace Whisperer.Service
{
    public interface IAnswerService : IService<Answer>
    {
        IEnumerable<Answer> GetByMeeting(Meeting meeting);
        Task<IEnumerable<Answer>> GetByMeeting(DateTime? date = default(DateTime?));
    }
}