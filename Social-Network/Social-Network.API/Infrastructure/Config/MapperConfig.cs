using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Social_Network.API.Infrastructure.Mappers;
using Social_Network.BLL.Infrastructure.Mappers;
using UserAccountMapper = Social_Network.BLL.Infrastructure.Mappers.UserAccountMapper;

namespace Social_Network.API.Infrastructure.Config
{
    public static class MapperConfig
    {
        public static void SetMapperDi(this IServiceCollection services) 
        {
            var configuration = new MapperConfiguration(opt => {
                opt.AddProfile(new UserAccountMapper());
                opt.AddProfile(new Mappers.UserAccountMapper());
                opt.AddProfile(new FriendMapper());
                opt.AddProfile(new UserEditMapper());
                opt.AddProfile(new NotificationMapper());
                opt.AddProfile(new NotificationResponseMapper());
                opt.AddProfile(new FriendViewModelMapper());
                opt.AddProfile(new MessageMapper());
                opt.AddProfile(new MessageViewModelMapper());
                opt.AddProfile(new PostMapper());
                opt.AddProfile(new PostViewModelMapper());
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
