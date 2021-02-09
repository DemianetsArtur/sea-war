using Social_Network.BLL.ModelsDto;
using System.Collections.Generic;

namespace Social_Network.BLL.Infrastructure.Interfaces
{
    public interface ICommentService
    {
        void CommentPostCreate(CommentPostDto entity);
        ICollection<CommentPostDto> CommentPostGet(string name);
        ICollection<CommentPostDto> CommentPostsGetAll();
    }
}