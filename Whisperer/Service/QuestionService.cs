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
    public class QuestionService : BaseService<Question>, IQuestionService
    {

        public QuestionService(IRepository<Question> repository) : base(repository)
        {
        }

        public Question GetByText(string text)
        {
            return _repository.Items.FirstOrDefault(x=> x.Text == text);
        }
    }
}