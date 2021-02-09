using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social_Network.API.Infrastructure.ViewModels.Friend;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFriendService _friendService;
        private readonly INotificationService _notificationService;

        public FriendController(IMapper mapper, 
                                IFriendService friendService, 
                                INotificationService notificationService)
        {
            this._mapper = mapper;
            this._friendService = friendService;
            this._notificationService = notificationService;
        }

        [HttpGet("get-user-all/{name}")]
        public IActionResult GetUserAll(string name)
        {
            return this.Ok(this._friendService.FriendAll(name));
        }

        [HttpPost]
        [Route("user-add-to-friends")]
        public IActionResult UserAddToFriends([FromBody] FriendViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var friendMapper = this._mapper.Map<FriendDto>(model);
            var notificationRemoveMapper = this._mapper.Map<NotificationDto>(model);
            
            this._friendService.UserAddToFriends(friendMapper);
            this._notificationService.EventAddToFriendRemove(notificationRemoveMapper);
            
            return this.Ok();
        }

        [HttpGet]
        [Route("get-users-in-friends")]
        public IActionResult GetUsersInFriendship()
        {
            var getUsersInFriendship = this._friendService.UsersInFriendship();
            if (getUsersInFriendship == null)
            {
                return this.Ok();
            }
            else
            {
                return this.Ok(getUsersInFriendship);
            }
        }

        [HttpPost]
        [Route("remove-from-friends")]
        public IActionResult RemoveFromFriends([FromBody] FriendRemoveViewModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return this.BadRequest(ModelState);
            }

            var friendMapper = this._mapper.Map<FriendDto>(model);
            this._friendService.UserInFriendshipRemove(friendMapper);

            return this.Ok();
        }


    }
}