using System.Threading.Tasks;
using Whisperer.Models;

namespace Whisperer.Service
{
    public interface IChannelService
    {
        Task<ChannelInfo> GetChannelInfo();
        Task<ChannelsList> GetChannelList();
    }
}