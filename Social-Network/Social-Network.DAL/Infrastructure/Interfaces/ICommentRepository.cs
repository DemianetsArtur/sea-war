using Social_Network.DAL.Entities;
using System.Collections.Generic;

namespace Social_Network.DAL.Infrastructure.Interfaces
{
    public interface ICommentRepository
    {
        void CommentPostCreate(CommentPost entity);
        ICollection<CommentPost> CommentPostGet(string name);
        ICollection<CommentPost> CommentPostsGetAll();
    }
}
