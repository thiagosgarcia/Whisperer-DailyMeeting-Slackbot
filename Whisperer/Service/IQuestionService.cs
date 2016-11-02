using Whisperer.Models;

namespace Whisperer.Service
{
    public interface IQuestionService: IService<Question>
    {
        Question GetByText(string text);
    }
}