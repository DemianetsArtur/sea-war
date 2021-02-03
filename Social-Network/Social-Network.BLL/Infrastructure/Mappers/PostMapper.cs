using AutoMapper;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;

namespace Social_Network.BLL.Infrastructure.Mappers
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            this.CreateMap<Post, PostDto>();
            this.CreateMap<PostDto, Post>();
        }
    }
}
