using AutoMapper;
using Social_Network.API.Infrastructure.ViewModels.Friend;
using Social_Network.API.Infrastructure.ViewModels.Notification;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Infrastructure.Mappers
{
    public class NotificationResponseMapper : Profile
    {
        public NotificationResponseMapper()
        {
            this.CreateMap<NotificationViewModel, NotificationDto>();
            this.CreateMap<NotificationDto, NotificationViewModel>();

            this.CreateMap<NotificationRemoveViewModel, NotificationDto>();
            this.CreateMap<NotificationDto, NotificationRemoveViewModel>();

            this.CreateMap<FriendViewModel, NotificationDto>();
            this.CreateMap<NotificationDto, FriendViewModel>();
        }
        
    }
}