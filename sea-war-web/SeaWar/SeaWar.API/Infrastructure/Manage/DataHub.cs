using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using SeaWar.BLL.Infrastructure.Interfaces;
using SeaWar.BLL.Infrastructure.Models;
using SeaWar.BLL.Infrastructure.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeaWar.API.Infrastructure.Manage
{
    public class DataHub : Hub
    {
        private readonly IPlayerService playerService;

        private readonly IMapper mapper;

        private readonly IConfiguration configuration;

        private ICollection<PlayerModel> Names = new List<PlayerModel>();

        public DataHub(IPlayerService playerService, 
                       IMapper mapper,
                       IConfiguration configuration)
        {
            this.playerService = playerService;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public override async Task OnConnectedAsync()
        {
            var player = this.playerService.GetAll();
            var playerCount = Int32.Parse(this.configuration["InfoOptions:UserCount"]);
            if (player.Count() == playerCount) 
            {
                await this.PlayersSendAsync();
            }
            await base.OnConnectedAsync();
        }

        public void CreateClient(PlayerModel entity) 
        {
            var players = this.playerService.GetAll();
            if (players.Count() <= 1) 
            {
                var player = this.mapper.Map<PlayerDto>(entity);
                player.ConnectionId = this.Context.ConnectionId;
                this.playerService.PlayerAdd(player);
            }
        }

        public void HitPointsInvoke(PlayerModel entity) 
        {
            var playerMapp = this.mapper.Map<PlayerDto>(entity);
            this.playerService.PlayerHitPointUpdate(playerMapp);
        }

        public async Task CountInvoke(PlayerModel entity) 
        {
            var playerMapp = this.mapper.Map<PlayerDto>(entity);
            this.playerService.PlayerCountUpdate(playerMapp);
            var msg = this.configuration["InfoOptions:msgPlayer"];
            var players = this.playerService.GetAll();
            await Clients.All.SendAsync(msg, players);
        }

        public async Task CoordinateCreate(PlayerModel entity) 
        {
            var playerMapp = this.mapper.Map<PlayerDto>(entity);
            this.playerService.PlayerCoordinateCreate(playerMapp);
            var msg = this.configuration["InfoOptions:coordinateCreate"];
            var players = this.playerService.GetAll();
            await this.Clients.All.SendAsync(msg, players);
        }

        public async Task PlayerRemove(PlayerModel entity) 
        {
            var playerMapp = this.mapper.Map<PlayerDto>(entity);
            this.playerService.PlayerRemove(playerMapp);
            await this.Clients.All.SendAsync("PlayerRemove", null);
        }

        private async Task PlayersSendAsync() 
        {
            var msg = this.configuration["InfoOptions:msgPlayer"];
            var players = this.playerService.GetAll();
            await Clients.All.SendAsync(msg, players);
        }

        public async Task NameCreate(PlayerModel entity) 
        {
            var msg = this.configuration["InfoOptions:nameCreate"];
            var player = this.playerService.GetAll();
            var playerValid = player.FirstOrDefault(opt => opt.Name == entity.Name);
            if (playerValid != null && player.Count() != 0)
            {
                await this.Clients.All.SendAsync(msg, null);

            }
            else {
                await this.Clients.All.SendAsync(msg, player);
            }
        }

        public async Task ClientsJoinedAsync(ChatMessage message) 
        {
            var msg = this.configuration["InfoOptions:clientsJoined"];
            await this.Clients.All.SendAsync(msg, message);
        }

        public async Task CoordinateSendAsync(CoordinateModel model) 
        {
            var msg = this.configuration["InfoOptions:coordinateSend"];
            await this.Clients.All.SendAsync(msg, model);
        }


    }
}
