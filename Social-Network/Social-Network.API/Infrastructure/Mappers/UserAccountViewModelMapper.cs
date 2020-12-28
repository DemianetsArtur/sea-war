using AutoMapper;
using Social_Network.API.Infrastructure.ViewModels.UserAccount;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Infrastructure.Mappers
{
    public class UserAccountMapper : Profile
    {
        public UserAccountMapper()
        {
            this.CreateMap<RegisterViewModel, UserAccountDto>();
            this.CreateMap<UserAccountDto, RegisterViewModel>();

            this.CreateMap<LoginViewModel, UserAccountDto>();
            this.CreateMap<UserAccountDto, LoginViewModel>();
        }
    }
}