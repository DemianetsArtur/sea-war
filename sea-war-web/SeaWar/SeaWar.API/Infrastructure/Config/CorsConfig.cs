using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SeaWar.API.Infrastructure.Config
{
    public static class CorsConfig
    {
        public static IServiceCollection SetCors(this IServiceCollection services, IConfiguration configuration) 
        {
            string corsPolisy = configuration["Cors:CorsPolicy"];
            string urlCors = configuration["Url:UrlCors"];
            return services.AddCors(opt => 
            {
                opt.AddPolicy(corsPolisy, builder => builder
                   .WithOrigins(urlCors)
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());
            });
        }
    }
}
