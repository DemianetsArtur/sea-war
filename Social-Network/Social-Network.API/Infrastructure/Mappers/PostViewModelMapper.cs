using AutoMapper;
using Social_Network.API.Infrastructure.ViewModels.Post;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Infrastructure.Mappers
{
    public class PostViewModelMapper : Profile
    {
        public PostViewModelMapper()
        {
            this.CreateMap<PostCreateViewModel, PostDto>();
            this.CreateMap<PostDto, PostCreateViewModel>();
        }
    }
}
