using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Whisperer.Models;

namespace Whisperer.Service
{
    public interface IQuestionService: IService<Question>
    {
        Task<IEnumerable<Question>> GetAll(bool? active = null);
        Question GetByText(string text);
        Task<DirectMessageResponse> Ask(ApiUser user, Question question);
        Task<DirectMessageHistoryResponse> TrackAnswer(DateTime timeout, DirectMessageResponse response, ApiUser user);
    }
}