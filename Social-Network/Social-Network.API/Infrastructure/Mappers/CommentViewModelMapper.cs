using AutoMapper;
using Social_Network.API.Infrastructure.ViewModels.Comment;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Infrastructure.Mappers
{
    public class CommentViewModelMapper : Profile
    {
        public CommentViewModelMapper()
        {
            this.CreateMap<CommentPostDto, CommentPostViewModel>();
            this.CreateMap<CommentPostViewModel, CommentPostDto>();
        }
    }
}
