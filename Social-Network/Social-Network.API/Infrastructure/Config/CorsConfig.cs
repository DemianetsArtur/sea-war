using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Social_Network.API.Infrastructure.Config
{
    public static class CorsConfig
    {
        public static void SetCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsPolicy = configuration["Cors:CorsPolicy"];
            var corsUrl = configuration["Cors:CorsUrl"];
            var corsUrlApp = configuration["Cors:CorsUrlApp"];
            services.AddCors(cors =>
            {
                cors.AddPolicy(corsPolicy, builder => builder
                    .WithOrigins(corsUrlApp, corsUrl)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
    }
}