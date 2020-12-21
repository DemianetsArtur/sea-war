using System;
using System.IO;
using System.Threading.Tasks;
using Social_Network.DAL.Infrastructure.Models;

namespace Social_Network.BLL.Infrastructure.Interfaces
{
    public interface IBlobStorageService
    {
        Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName);
        Task<Uri> GetFileFromBlobAsync(string fileName);
    }
}