using Microsoft.Extensions.DependencyInjection;
using Social_Network.API.Models.UsersInfo;

namespace Social_Network.API.Infrastructure.Config
{
    public static class AuthorizationConfig
    {
        public static void SetAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy(RoleInfo.Admin, UserPolicies.AdminPolicy());
                cfg.AddPolicy(RoleInfo.User, UserPolicies.UserPolicy());
            });
        }
    }
}