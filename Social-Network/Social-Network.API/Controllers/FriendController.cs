using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social_Network.BLL.Infrastructure.Interfaces;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFriendService _friendService;

        public FriendController(IMapper mapper, 
                                IFriendService friendService)
        {
            this._mapper = mapper;
            this._friendService = friendService;
        }

        [HttpGet("get-user-all/{name}")]
        public IActionResult GetUserAll(string name)
        {
            return this.Ok(this._friendService.FriendAll(name));
        }
    }
}