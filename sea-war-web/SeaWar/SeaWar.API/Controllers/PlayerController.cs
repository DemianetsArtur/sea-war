using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeaWar.BLL.Infrastructure.Interfaces;
using SeaWar.BLL.Infrastructure.Models;
using SeaWar.BLL.Infrastructure.ModelsDto;

namespace SeaWar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService playerService;

        private readonly IMapper mapper;

        public PlayerController(IPlayerService playerService, 
                                IMapper mapper)
        {
            this.playerService = playerService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("player-create")]
        public IActionResult PlayerPost([FromBody] PlayerModel entity) 
        {
            if (ModelState.IsValid)
            {
                var playerMapp = this.mapper.Map<PlayerModel, PlayerDto>(entity);
                this.playerService.PlayerAdd(playerMapp);
                return this.Ok();
            }
            else 
            {
                return this.BadRequest();
            }
            
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAll() 
        {
            return this.Ok(this.playerService.GetAll());
        }
    }
}
