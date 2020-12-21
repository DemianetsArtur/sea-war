using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using Social_Network.DAL.Infrastructure.Models;

namespace Social_Network.DAL.Infrastructure.Interfaces
{
    public interface IBlobStorageRepository
    {
        Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName);
        Task<Uri> GetFileFromBlobAsync(string fileName);
    }
}