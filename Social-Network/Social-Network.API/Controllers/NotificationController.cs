using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Social_Network.API.Infrastructure.Manages.HubConnect;
using Social_Network.API.Infrastructure.ViewModels.Notification;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IHubContext<HubConnect> _hubContext;
        private readonly IConfiguration _configuration;

        public NotificationController(IMapper mapper, 
                                      INotificationService notificationService, 
                                      IHubContext<HubConnect> hubContext, 
                                      IConfiguration configuration)
        {
            this._mapper = mapper;
            this._notificationService = notificationService;
            this._hubContext = hubContext;
            this._configuration = configuration;
        }   
        
        
        [HttpPost]
        [Route("event-add-to-friend")]
        public IActionResult AddToFriend([FromBody] NotificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var notificationMapper = this._mapper.Map<NotificationDto>(model);
            this._notificationService.EventAddToFriend(notificationMapper);
            return this.Ok();
        }

        [HttpPost]
        [Route("event-messages-create")]
        public IActionResult EventMessagesCreate([FromBody] NotificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var notificationMapper = this._mapper.Map<NotificationDto>(model);
            var notificationGet = this._notificationService.EventMessagesGet(notificationMapper);
            if (notificationGet != null)
            {
                this._notificationService.EventMessagesRemove(notificationMapper);
            }
            this._notificationService.EventMessageCreate(notificationMapper);
            return this.Ok();
        }

        [HttpPost]
        [Route("get-event-add-to-friend")]
        public async Task<IActionResult> GetAddToFriend([FromBody] NotificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }
            
            var getEventAddToFriends = this._notificationService.GetEventAddToFriend();
            var nameResponse = this._configuration["HubInfo:GetEventAddToFriends"];
            await this._hubContext.Clients.All.SendAsync(nameResponse, getEventAddToFriends);
            return this.Ok();
        }

        [HttpPost]
        [Route("remove-event-add-to-friend")]
        public IActionResult RemoveAddToFriend([FromBody] NotificationRemoveViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var notificationMapper = this._mapper.Map<NotificationDto>(model);
            this._notificationService.EventAddToFriendRemove(notificationMapper);
            return this.Ok();
        }

        [HttpPost]
        [Route("event-messages-remove")]
        public IActionResult EventMessagesRemove([FromBody] NotificationRemoveViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }
            
            var notificationMapper = this._mapper.Map<NotificationDto>(model);
            this._notificationService.EventMessagesRemove(notificationMapper);
            return this.Ok();
        }
    }
}