using AutoMapper;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.Models;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using System;

namespace Social_Network.BLL.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUoW _database;
        private readonly IMapper _mapper;

        public UserAccountService(IUoW database, IMapper mapper)
        {
            this._database = database;
            this._mapper = mapper;
        }

        public UserAccountDto UserAccountLoginFind(UserAccountDto entity)
        {
            var userMapper = this._mapper.Map<UserAccount>(entity);
            var user = this._database.UserAccount.UserAccountLoginFind(userMapper);
            return this._mapper.Map<UserAccountDto>(user);
        }

        public bool UserAccountFind(UserAccountDto entity)
        {
            var userMapper = this._mapper.Map<UserAccount>(entity);
            return this._database.UserAccount.UserAccountFind(userMapper);
        }

        public void UserAccountCreate(UserAccountDto entity) 
        {
            var userMapper = this._mapper.Map<UserAccount>(entity);
            var dateFormat = System.Globalization.DateTimeFormatInfo.InvariantInfo;
            var date = DateTime.Now.ToString(OptionsInfo.DateConfig, dateFormat);
            var guidKey = Guid.NewGuid().ToString();

            userMapper.PartitionKey = date;
            userMapper.RowKey = guidKey;
            this._database.UserAccount.UserAccountCreate(userMapper);
        }
    }
}
