using AutoMapper;
using Social_Network.API.Infrastructure.ViewModels.Friend;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Infrastructure.Mappers
{
    public class FriendViewModelMapper : Profile
    {
        public FriendViewModelMapper()
        {
            this.CreateMap<FriendViewModel, FriendDto>();
            this.CreateMap<FriendDto, FriendViewModel>();

            this.CreateMap<FriendRemoveViewModel, FriendDto>();
            this.CreateMap<FriendDto, FriendRemoveViewModel>();
        }
    }
}