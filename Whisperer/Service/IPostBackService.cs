using System.Threading.Tasks;
using Whisperer.Models;

namespace Whisperer.Service
{
    public interface IPostBackService
    {
        Task<string> Ping();
        Task<CustomOutgoingPostData> Command(IncomingPostData data);
    }
}