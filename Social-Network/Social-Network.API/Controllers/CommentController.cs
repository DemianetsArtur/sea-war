using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social_Network.API.Infrastructure.ViewModels.Comment;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, 
                                 IMapper mapper)
        {
            this._commentService = commentService;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("comment-post-create")]
        public IActionResult CommentPostCreate([FromBody] CommentPostViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var commentMapper = this._mapper.Map<CommentPostDto>(model);
            this._commentService.CommentPostCreate(commentMapper);
            return this.Ok();
        }

        [HttpGet("comment-post-get/{name}")]
        public IActionResult CommentPostGet(string name) 
        {
            if (string.IsNullOrEmpty(name)) 
            {
                return this.BadRequest();
            }

            var comments = this._commentService.CommentPostGet(name);
            return this.Ok(comments);
        }
    }
}
