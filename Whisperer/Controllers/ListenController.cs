using System.Threading.Tasks;
using System.Web.Http;
using StructureMap;
using Whisperer.App_Start;
using Whisperer.DependencyResolution;
using Whisperer.Models;
using Whisperer.Service;

namespace Whisperer.Controllers
{
    [AllowAnonymous]
    public class ListenController : ApiController
    {
        private readonly IPostBackService _service;
        public ListenController(IPostBackService service)
        {
            _service = service;
        }
        public async Task<CustomOutgoingPostData> Post([FromBody]IncomingPostData data)
        {
            return await _service.Command(data);
        }
        [HttpPost]
        [Route("api/test/ping/")]
        public async Task<string> TestMessage([FromBody]IncomingPostData data)
        {
            return await _service.Ping();
        }
        [HttpPost]
        [Route("api/test/command/")]
        public async Task<CustomOutgoingPostData> TestCommand([FromBody]IncomingPostData data)
        {
            return await _service.Command(data);
        }
    }
}
