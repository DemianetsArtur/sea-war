using AutoMapper;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;

namespace Social_Network.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUoW _database;
        private readonly IMapper _mapper;

        public NotificationService(IUoW database, IMapper mapper)
        {
            this._database = database;
            this._mapper = mapper;
        }

        public void NotificationCreate(NotificationDto entity)
        {
            var notificationMapp = this._mapper.Map<Notification>(entity);
            this._database.Notification.NotificationCreate(notificationMapp);
        }
    }
}