using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Social_Network.API.Infrastructure.Manages.HubConnect;
using Social_Network.API.Infrastructure.ViewModels.Message;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;
        private readonly IHubContext<HubConnect> _hubContext;
        private readonly IConfiguration _configuration;

        public MessageController(IMapper mapper, 
                                 IMessageService messageService, 
                                 IHubContext<HubConnect> hubContext, 
                                 IConfiguration configuration)
        {
            this._mapper = mapper;
            this._messageService = messageService;
            this._hubContext = hubContext;
            this._configuration = configuration;
        }
        
        [HttpPost]
        [Route("message-create")]
        public IActionResult MessageCreate([FromBody] MessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var messageMapper = this._mapper.Map<MessageDto>(model);
            this._messageService.MessageCreate(messageMapper);
            return this.Ok();
        }

        [HttpPost]
        [Route("message-all-get")]
        public async Task<IActionResult> MessageAllGet([FromBody] MessageGetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var nameResponse = this._configuration["HubInfo:GetMessageAll"];
            var messageMapper = this._mapper.Map<MessageDto>(model);
            var messageAll = this._messageService.MessageAll(messageMapper);
            await this._hubContext.Clients.All.SendAsync(nameResponse, messageAll);
            return this.Ok(messageAll);
        }
    }
}