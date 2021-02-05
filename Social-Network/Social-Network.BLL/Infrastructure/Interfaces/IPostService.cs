using Social_Network.BLL.ModelsDto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Social_Network.BLL.Infrastructure.Interfaces
{
    public interface IPostService
    {
        Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName);
        void PostCreate(PostDto entity);
        ICollection<PostDto> PostsGet(string name);
    }
}
