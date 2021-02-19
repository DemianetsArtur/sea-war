using AutoMapper;
using Social_Network.API.Infrastructure.ViewModels.Edit;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Infrastructure.Mappers
{
    public class UserEditMapper : Profile
    {
        public UserEditMapper()
        {
            this.CreateMap<UserAccountDto, EditViewModel>();
            this.CreateMap<EditViewModel, UserAccountDto>();

            this.CreateMap<EditProfileViewModel, UserAccountDto>();
        }
    }
}