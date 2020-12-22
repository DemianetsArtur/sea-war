using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Social_Network.BLL.Infrastructure.Interfaces;
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
    }
}