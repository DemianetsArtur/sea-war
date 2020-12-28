using AutoMapper;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;

namespace Social_Network.BLL.Infrastructure.Mappers
{
    public class FriendMapper : Profile
    {
        public FriendMapper()
        {
            this.CreateMap<Friend, FriendDto>();
            this.CreateMap<FriendDto, Friend>();
        }
    }
}