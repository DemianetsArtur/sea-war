using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SeaWar.BLL.Infrastructure.Mappers;

namespace SeaWar.API.Infrastructure.Config
{
    public static class MappConfig
    {
        public static void SetMapp(this IServiceCollection services) 
        {
            MapperConfiguration mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PlayerMapp());
            });

            IMapper map = mapper.CreateMapper();
            services.AddSingleton(map);
        }
    }
}
