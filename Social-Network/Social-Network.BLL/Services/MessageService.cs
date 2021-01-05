using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.Models;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;

namespace Social_Network.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUoW _database;
        private readonly IMapper _mapper;

        public MessageService(IUoW database, 
                              IMapper mapper)
        {
            this._database = database;
            this._mapper = mapper;
        }

        public void MessageCreate(MessageDto entity)
        {
            var timeFormat = System.Globalization.DateTimeFormatInfo.InvariantInfo;
            var dateFormat = System.Globalization.DateTimeFormatInfo.InvariantInfo;
            var time = DateTime.Now.ToString(OptionsInfo.TimeConfig, timeFormat);
            var date = DateTime.Now.ToString(OptionsInfo.DateConfig, dateFormat);
            var guidKey = Guid.NewGuid().ToString();
            var messageMapper = this._mapper.Map<Message>(entity);
            messageMapper.PartitionKey = date;
            messageMapper.RowKey = guidKey;
            messageMapper.Time = time;
            this._database.Message.MessageCreate(messageMapper);
        }

        public ICollection<MessageDto> MessageAll(MessageDto entity)
        {
            var messageInfoMapper = this._mapper.Map<Message>(entity);
            var messageAll = this._database.Message.MessageAll(messageInfoMapper);
            return this._mapper.Map<ICollection<MessageDto>>(messageAll);
        }
    }
}