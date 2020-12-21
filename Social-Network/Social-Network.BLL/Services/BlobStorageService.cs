using System;
using System.IO;
using System.Threading.Tasks;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Models;

namespace Social_Network.BLL.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IUoW _database;

        public BlobStorageService(IUoW database)
        {
            this._database = database;
        }

        public async Task<Uri> GetFileFromBlobAsync(string fileName)
        {
            return await this._database.BlobStorage.GetFileFromBlobAsync(fileName);
        }

        public Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName)
        {
            return this._database.BlobStorage.FileUploadToBlobAsync(content, contentType, fileName);
        }
    }
}