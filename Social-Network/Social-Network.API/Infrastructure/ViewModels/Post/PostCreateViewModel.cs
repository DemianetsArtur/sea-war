using Microsoft.AspNetCore.Http;

namespace Social_Network.API.Infrastructure.ViewModels.Post
{
    public class PostCreateViewModel
    {
        public string Name { get; set; }
        public string PostText { get; set; }
        public string NameContent { get; set; }
        public IFormFile Content { get; set; }
    }
}
