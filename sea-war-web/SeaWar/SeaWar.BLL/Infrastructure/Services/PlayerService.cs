using AutoMapper;
using SeaWar.BLL.Infrastructure.Interfaces;
using SeaWar.BLL.Infrastructure.ModelsDto;
using SeaWar.DAL.Entities;
using SeaWar.DAL.Interfaces;
using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUoW database;

        private readonly IMapper mapper;

        public PlayerService(IUoW database, 
                             IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public void PlayerHitPointUpdate(PlayerDto entity) 
        {
            Player playerMapp = this.mapper.Map<Player>(entity);
            this.database.playerRepository.HitPointUpdate(playerMapp);
        }

        public void PlayerCoordinateCreate(PlayerDto entity) 
        {
            Player playerMapp = this.mapper.Map<Player>(entity);
            this.database.playerRepository.CoordinateCreate(playerMapp);
        }

        public void PlayerCountUpdate(PlayerDto entity) 
        {
            Player playerMapp = this.mapper.Map<Player>(entity);
            this.database.playerRepository.CountUpdate(playerMapp);
        }

        public void PlayerAdd(PlayerDto entity) 
        {
            Player playerMapp = this.mapper.Map<Player>(entity);
            this.database.playerRepository.Add(playerMapp);
        }

        public ICollection<PlayerDto> GetAll() 
        {
            var player = this.database.playerRepository.GetAll();
            return this.mapper.Map<ICollection<PlayerDto>>(player);
        }
    }
}
