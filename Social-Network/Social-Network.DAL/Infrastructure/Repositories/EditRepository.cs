using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Cosmos.Table;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Models;
using Social_Network.DAL.Manages.Tables;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Social_Network.DAL.Infrastructure.Repositories
{
    public class EditRepository : IEditRepository
    {
        private readonly TableManage _tableManage;
        private readonly BlobServiceClient _blobServiceClient;

        public EditRepository(TableManage tableManage, 
                              BlobServiceClient blobServiceClient)
        {
            this._tableManage = tableManage;
            this._blobServiceClient = blobServiceClient;
        }

        public async Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName)
        {
            var containerClient = this.GetContainerClient();
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
            return blobClient.Uri;
        }

        public void UserProfileEdit(UserAccount entity) 
        {
            var cloudTable = TableResponse.GetTable(this._tableManage.StorageKey, StorageInfo.UserAccountTable);
            var operation = TableOperation.InsertOrReplace(entity);
            cloudTable.Execute(operation);
        }

        public async Task FileRemoveToBlobAsync(string fileName) 
        {
            if (!string.IsNullOrEmpty(fileName)) 
            {
                var containerClient = this.GetContainerClient();
                var blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.DeleteAsync();
            }
        }

        private BlobContainerClient GetContainerClient()
        {
            var containerClient = this._blobServiceClient.GetBlobContainerClient(StorageInfo.ContainerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            return containerClient;
        }
    }
}
