using System.Threading.Tasks;
using Whisperer.Models;

namespace Whisperer.Service
{
    public interface IUserService : IService<User>
    {
        Task<int> AddNewUsers(UsersList users);
        User GetByUserId(string userId);
        User GetByUsername(string username);
        Task<UsersList> GetUsers();
    }
}