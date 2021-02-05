using Social_Network.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Social_Network.DAL.Infrastructure.Interfaces
{
    public interface IPostRepository
    {
        Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName);
        void PostCreate(Post entity);
        ICollection<Post> PostsGet(string name);
    }
}
