using AutoMapper;
using SeaWar.BLL.Infrastructure.Models;
using SeaWar.BLL.Infrastructure.ModelsDto;
using SeaWar.DAL.Entities;

namespace SeaWar.BLL.Infrastructure.Mappers
{
    public class PlayerMapp : Profile
    {
        public PlayerMapp()
        {
            this.CreateMap<Player, PlayerDto>();
            this.CreateMap<PlayerDto, Player>();

            this.CreateMap<PlayerModel, PlayerDto>();
            this.CreateMap<PlayerDto, PlayerModel>();

            this.CreateMap<ChatMessage, PlayerDto>()
                .ForMember(opt => opt.Name, q => q.MapFrom(w => w.Name));
            this.CreateMap<PlayerDto, ChatMessage > ()
                .ForMember(opt => opt.Name, q => q.MapFrom(w => w.Name));

        }
    }
}
