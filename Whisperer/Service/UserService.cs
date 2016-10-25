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
    public class UserService : BaseService<User>, IUserService
    {
        private readonly Configuration _configuration;
        private IService<User> _userService;

        public UserService(IRepository<User> repository) : base(repository)
        {
            _configuration = Ioc.Container.GetInstance<Configuration>();
        }
        public async Task<UsersList> GetUsers()
        {
            using (var client = new HttpClient())
            {
                var parameters = new OutgoingUserParameters
                {
                    token = _configuration.GetAppToken(),
                    presence = 1
                };
                var content = new FormUrlEncodedContent(parameters.ToPairs());
                var response = await client.PostAsync("https://slack.com/api/users.list", content);
                var users = await response.Content.ReadAsAsync<UsersList>();

                await AddNewUsers(users);

                return users;
            }
        }
        public async Task<int> AddNewUsers(UsersList users)
        {
            var count = 0;
            users.members.ForEach(x =>
            {
                var user = GetByUserId(x.id);
                if (user != null)
                    return;

                Add(Mapper.Map.Map<ApiUser, User>(x));
                count++;
            });
            return count;
        }

        public User GetByUserId(string userId)
        {
            return _repository.Items.FirstOrDefault(x => x.UserId == userId);
        }

        public User GetByUsername(string username)
        {
            return _repository.Items.FirstOrDefault(x => x.Username == username);
        }
    }
}