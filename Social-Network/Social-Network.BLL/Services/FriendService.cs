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
    public class FriendService : IFriendService
    {
        private readonly IMapper _mapper;
        private readonly IUoW _database;

        public FriendService(IMapper mapper, 
                             IUoW database)
        {
            this._mapper = mapper;
            this._database = database;
        }
        
        
        public ICollection<UserAccountDto> FriendAll(string name)
        {
            return this._mapper.Map<ICollection<UserAccountDto>>(this._database.Friend.FriendAll(name));
        }

        public void UserAddToFriends(FriendDto entity)
        {
            var dateFormat = System.Globalization.DateTimeFormatInfo.InvariantInfo;
            var date = DateTime.Now.ToString(OptionsInfo.DateConfig, dateFormat);
            var guidKey = Guid.NewGuid().ToString();
            var friendMapper = this._mapper.Map<Friend>(entity);
            friendMapper.PartitionKey = date;
            friendMapper.RowKey = guidKey;
            this._database.Friend.UserAddToFriends(friendMapper);
        }

        public ICollection<FriendDto> UsersInFriendship()
        {
            return this._mapper.Map<ICollection<FriendDto>>(this._database.Friend.UsersInFriendship());
        }
    }
}