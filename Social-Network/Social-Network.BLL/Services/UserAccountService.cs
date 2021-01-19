using AutoMapper;
using Microsoft.Extensions.Logging;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.Models;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace Social_Network.BLL.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUoW _database;
        private readonly IMapper _mapper;
        private readonly ILogger<UserAccountService> _log;

        public UserAccountService(IUoW database, 
                                  IMapper mapper,
                                  ILogger<UserAccountService> log)
        {
            this._database = database;
            this._mapper = mapper;
            this._log = log;
        }

        public UserAccountDto UserAccountLoginFind(UserAccountDto entity)
        {
            var userMapper = this._mapper.Map<UserAccount>(entity);
            var userGet = this._database.UserAccount.UserGet(userMapper.Name);
            if (userGet != null && BCrypt.Net.BCrypt.Verify(entity.Password, userGet.Password))
            {
                return this._mapper.Map<UserAccountDto>(userGet);
            }
            else 
            {
                return null;
            }
        }

        public void UserAccountReplace(string name, string imagePath)
        {
            this._log.LogInformation("Request UserAccountReplace");
            this._database.UserAccount.UserAccountReplace(name, imagePath);
        }

        public bool UserAccountFind(UserAccountDto entity)
        {
            var userMapper = this._mapper.Map<UserAccount>(entity);
            this._log.LogInformation("Request UserAccountFind");
            return this._database.UserAccount.UserAccountFind(userMapper);
        }

        public UserAccountDto GetUser(string name)
        {
            this._log.LogInformation("Request GetUser");
            return this._mapper.Map<UserAccountDto>(this._database.UserAccount.UserGet(name));
        }

        public void UserAccountCreate(UserAccountDto entity) 
        {
            var userMapper = this._mapper.Map<UserAccount>(entity);
            var dateFormat = System.Globalization.DateTimeFormatInfo.InvariantInfo;
            var date = DateTime.Now.ToString(OptionsInfo.TimeConfig, dateFormat);
            var guidKey = Guid.NewGuid().ToString();

            userMapper.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            userMapper.PartitionKey = date;
            userMapper.RowKey = guidKey;
            this._log.LogInformation("Request UserAccountCreate");
            this._database.UserAccount.UserAccountCreate(userMapper);
        }

        public void UserChangedCreate(UserAccountDto entity)
        {
            var user = this._database.UserAccount.UserGet(entity.Name);
            var userMapp = this._mapper.Map<UserAccount>(entity);
            userMapp.Password = user.Password;
            userMapp.UserType = user.UserType;
            userMapp.PartitionKey = user.PartitionKey;
            userMapp.RowKey = user.RowKey;
            this._log.LogInformation("Request UserChangedCreate");
            this._database.UserAccount.UserAccountCreate(userMapp);
        }

        public ICollection<UserAccountDto> UserAll()
        {
            this._log.LogInformation("Request UserAll");
            return this._mapper.Map<ICollection<UserAccountDto>>(this._database.UserAccount.UserAll());
        }
    }
}
