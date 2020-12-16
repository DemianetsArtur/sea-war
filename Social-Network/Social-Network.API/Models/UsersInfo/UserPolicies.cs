using Microsoft.AspNetCore.Authorization;

namespace Social_Network.API.Models.UsersInfo
{
    public class UserPolicies
    {
        public static AuthorizationPolicy AdminPolicy() 
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole(RoleInfo.Admin)
                                                   .Build();
        }

        public static AuthorizationPolicy UserPolicy() 
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole(RoleInfo.User)
                                                   .Build();
        }
    }
}
