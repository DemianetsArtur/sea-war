using Social_Network.DAL.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Social_Network.DAL.Infrastructure.Interfaces
{
    public interface IEditRepository
    {
        void UserProfileEdit(UserAccount entity);
        Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName);
        Task FileRemoveToBlobAsync(string fileName);
    }
}
