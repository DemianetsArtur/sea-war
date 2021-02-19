using Social_Network.BLL.ModelsDto;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Social_Network.BLL.Infrastructure.Interfaces
{
    public interface IEditService
    {
        void UserProfileEdit(UserAccountDto entity);
        Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName);
        Task FileRemoveToBlobAsync(string fileName);
    }
}
