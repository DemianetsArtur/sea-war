using AutoMapper;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Social_Network.BLL.Services
{
    public class EditService : IEditService
    {
        private readonly IUoW _database;
        private readonly IMapper _mapper;

        public EditService(IUoW database, 
                           IMapper mapper)
        {
            this._database = database;
            this._mapper = mapper;
        }

        public Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName)
        {
            return this._database.Edit.FileUploadToBlobAsync(content, contentType, fileName);
        }

        public async Task FileRemoveToBlobAsync(string fileName) 
        {
            await this._database.Edit.FileRemoveToBlobAsync(fileName);
        }

        public void UserProfileEdit(UserAccountDto entity) 
        {
            var userGet = this._database.UserAccount.UserGet(entity.Name);
            var userMapper = this._mapper.Map<UserAccount>(entity);

            userMapper.Password = userGet.Password;
            userMapper.UserType = userGet.UserType;
            userMapper.PartitionKey = userGet.PartitionKey;
            userMapper.RowKey = userGet.RowKey;
            userMapper.EmailConfirmed = userGet.EmailConfirmed;
            userMapper.EmailDateKey = userGet.EmailDateKey;
            userMapper.EmailKey = userGet.EmailKey;

            if (string.IsNullOrEmpty(entity.ImagePath)) 
            {
                userMapper.ImagePath = userGet.ImagePath;    
            }

            this._database.Edit.UserProfileEdit(userMapper);
        }
    }
}
