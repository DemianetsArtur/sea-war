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

        public NotificationDto EventMessagesGet(NotificationDto entity)
        {
            var notificationMapper = this._mapper.Map<Notification>(entity);
            var notificationGet =
                this._mapper.Map<NotificationDto>(this._database.Notification.EventMessagesGet(notificationMapper));
            return notificationGet;
        }

        public void EventAddToFriend(NotificationDto entity)
        {
            var dateFormat = System.Globalization.DateTimeFormatInfo.InvariantInfo;
            var date = DateTime.Now.ToString(OptionsInfo.TimeConfig, dateFormat);
            var guidKey = Guid.NewGuid().ToString();
            var notificationMapper = this._mapper.Map<Notification>(entity);
            notificationMapper.PartitionKey = date;
            notificationMapper.RowKey = guidKey;
            this._database.Notification.EventAddToFriend(notificationMapper);
        }

        public void EventMessageCreate(NotificationDto entity)
        {
            var dateFormat = System.Globalization.DateTimeFormatInfo.InvariantInfo;
            var date = DateTime.Now.ToString(OptionsInfo.TimeConfig, dateFormat);
            var guidKey = Guid.NewGuid().ToString();
            var notificationMapper = this._mapper.Map<Notification>(entity);
            notificationMapper.PartitionKey = date;
            notificationMapper.RowKey = guidKey;
            this._database.Notification.EventMessageCreate(notificationMapper);
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

        public void EventMessagesRemove(NotificationDto entity)
        {
            var notificationMapper = this._mapper.Map<Notification>(entity);
            this._database.Notification.EventMessagesRemove(notificationMapper);
        }
    }
}