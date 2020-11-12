using AutoMapper;
using SeaWar.BLL.Infrastructure.Models;
using SeaWar.DAL.Infrastructure.Entities;

namespace SeaWar.BLL.Infrastructure.Mappers
{
    public class SetMapperProfile : Profile
    {
        public SetMapperProfile()
        {
            this.CreateMap<Ship, ShipDTO>();
            this.CreateMap<ShipDTO, Ship>();
        }
    }
}
