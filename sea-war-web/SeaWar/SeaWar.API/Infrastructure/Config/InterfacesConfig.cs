using Microsoft.Extensions.DependencyInjection;
using SeaWar.API.Infrastructure.Interfaces;
using SeaWar.BLL.Infrastructure.Interfaces;
using SeaWar.BLL.Infrastructure.Services;
using SeaWar.DAL.DomainModel;
using SeaWar.DAL.Interfaces;
using SeaWar.DAL.Repository;

namespace SeaWar.API.Infrastructure.Config
{
    public static class InterfacesConfig
    {
        public static IServiceCollection SetInterfacesDI(this IServiceCollection services) 
        {
            services.AddSingleton<IDataManage, DataManage>();
            services.AddSingleton<IPlayerRepository, PlayerRepository>();
            services.AddSingleton<IUoW, UoW>();
            services.AddSingleton<IPlayerService, PlayerService>();

            return services;
        }
    }
}
