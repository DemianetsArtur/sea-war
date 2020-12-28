using System;
using System.Collections.Generic;
using AutoMapper;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.Models;
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

        public void EventAddToFriend(NotificationDto entity)
        {
            var dateFormat = System.Globalization.DateTimeFormatInfo.InvariantInfo;
            var date = DateTime.Now.ToString(OptionsInfo.DateConfig, dateFormat);
            var guidKey = Guid.NewGuid().ToString();
            var notificationMapp = this._mapper.Map<Notification>(entity);
            notificationMapp.PartitionKey = date;
            notificationMapp.RowKey = guidKey;
            this._database.Notification.EventAddToFriend(notificationMapp);
        }

        public ICollection<NotificationDto> GetEventAddToFriend()
        {
            return this._mapper.Map<ICollection<NotificationDto>>(
                this._database.Notification.GetEventAddToFriend());
        }

        public void EventAddToFriendRemove(NotificationDto entity)
        {
            var notificationMapper = this._mapper.Map<Notification>(entity);
            this._database.Notification.EventAddToFriendRemove(notificationMapper);
        }
    }
}