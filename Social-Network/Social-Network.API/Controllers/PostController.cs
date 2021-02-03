using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social_Network.API.Infrastructure.ViewModels.Post;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.ModelsDto;
using System;
using System.Threading.Tasks;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            this._postService = postService;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("post-content-create")]
        public async Task<IActionResult> PostContentCreate([FromForm] PostCreateViewModel model) 
        {
            var nameFile = model.Name + Guid.NewGuid().ToString();

            if (model.Content != null) 
            {
                var fileInfo = await this._postService.FileUploadToBlobAsync(model.Content.OpenReadStream(), model.Content.ContentType, nameFile);
                model.NameContent = fileInfo.AbsoluteUri;
            }
            var postMapper = this._mapper.Map<PostDto>(model);
            this._postService.PostCreate(postMapper);
            return this.Ok();
        }

        [HttpGet("posts-get/{name}")]
        public IActionResult PostsGet(string name) 
        {
            if (string.IsNullOrEmpty(name)) 
            {
                return this.BadRequest();
            }

            var posts = this._postService.PostsGet(name);
            return this.Ok(posts);
        }
    }
}
