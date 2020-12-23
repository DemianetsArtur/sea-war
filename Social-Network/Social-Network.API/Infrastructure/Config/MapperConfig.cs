using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Social_Network.API.Infrastructure.Mappers;
using Social_Network.DAL.Infrastructure.Mappers;
using UserAccountMapper = Social_Network.BLL.Infrastructure.EntitiesMapp.UserAccountMapper;

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
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
