using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Models;

namespace Social_Network.DAL.Infrastructure.Repositories
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageRepository(BlobServiceClient blobServiceClient)
        {
            this._blobServiceClient = blobServiceClient;
        }    
        
        public async Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName)
        {
            var containerClient = this.GetContainerClient();
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(content, new BlobHttpHeaders {ContentType = contentType});
            return blobClient.Uri;
        }

        public async Task<Uri> GetFileFromBlobAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(StorageInfo.ContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var entity = await blobClient.DownloadAsync();
            return blobClient.Uri;
            //return new DownloadFileInfo{ Content = entity.Value.Content, ContentType = entity.Value.ContentType};
        }

        private BlobContainerClient GetContainerClient()
        {
            var containerClient = this._blobServiceClient.GetBlobContainerClient(StorageInfo.ContainerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            return containerClient;
        }
    
    }
}