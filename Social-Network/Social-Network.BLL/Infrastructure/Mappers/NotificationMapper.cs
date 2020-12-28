using AutoMapper;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;

namespace Social_Network.BLL.Infrastructure.Mappers
{
    public class NotificationMapper : Profile
    {
        public NotificationMapper()
        {
            this.CreateMap<Notification, NotificationDto>();
            this.CreateMap<NotificationDto, Notification>();
        }
    }
}