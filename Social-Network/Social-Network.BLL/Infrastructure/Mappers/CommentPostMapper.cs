using AutoMapper;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;

namespace Social_Network.BLL.Infrastructure.Mappers
{
    public class CommentPostMapper : Profile
    {
        public CommentPostMapper()
        {
            this.CreateMap<CommentPost, CommentPostDto>();
            this.CreateMap<CommentPostDto, CommentPost>();
        }
    }
}
