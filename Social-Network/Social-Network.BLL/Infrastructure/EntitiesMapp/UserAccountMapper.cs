using AutoMapper;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;

namespace Social_Network.BLL.Infrastructure.EntitiesMapp
{
    public class UserAccountMapper : Profile
    {
        public UserAccountMapper()
        {
            this.CreateMap<UserAccount, UserAccountDto>();
            this.CreateMap<UserAccountDto, UserAccount>();
        }
    }
}
