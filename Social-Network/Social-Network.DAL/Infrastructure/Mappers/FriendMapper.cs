using AutoMapper;
using Social_Network.DAL.Entities;

namespace Social_Network.DAL.Infrastructure.Mappers
{
    public class FriendMapper : Profile
    {
        public FriendMapper()
        {
            this.CreateMap<Friend, UserAccount>();
            this.CreateMap<UserAccount, Friend>();
        }
        
    }
}