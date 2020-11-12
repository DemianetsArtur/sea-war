using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SeaWar.BLL.Infrastructure.Mappers;

namespace SeaWar.UI.Infrastructure.Config.ConfigMapper
{
    public static class AddMapper
    {
        public static IServiceCollection SetMapper(this IServiceCollection services) 
        {
            MapperConfiguration mapperCfg = new MapperConfiguration(cfg => {
                cfg.AddProfile(new SetMapperProfile());
            });

            IMapper mapper = mapperCfg.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
