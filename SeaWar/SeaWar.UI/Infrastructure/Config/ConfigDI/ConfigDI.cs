using Microsoft.Extensions.DependencyInjection;
using SeaWar.BLL.Infrastructure.Interfaces;
using SeaWar.BLL.Infrastructure.Services;
using SeaWar.UI.Infrastructure.Interfaces;
using SeaWar.UI.Infrastructure.Services;

namespace SeaWar.UI.Infrastructure.Config.ConfigDI
{
    public static class ConfigDI
    {
        public static IServiceCollection SetDI(this IServiceCollection services) 
        {
            services.AddSingleton<IServiceMessageAddShip, ServiceMessageAddShips>();
            services.AddSingleton<IServicePlayer, ServicePlayer>();
            services.AddSingleton<IServiceGame, ServiceGame>();
            return services;
        }
    }
}
