using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Social_Network.API.Infrastructure.Config
{
    public static class JwtConfig
    {
        public static void SetJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            var issuer = configuration["Jwt:IssuerApp"];
            var audience = configuration["Jwt:AudienceApp"];
            var issuerDev = configuration["Jwt:Issuer"];
            var audienceDev = configuration["Jwt:Audience"];
            var key = configuration["Jwt:SecretKey"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(ber =>
                    {
                        ber.RequireHttpsMetadata = false;
                        ber.SaveToken = true;
                        ber.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = issuerDev,
                            ValidAudience = audienceDev,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                            ClockSkew = TimeSpan.Zero
                        };
                    });
        }
    }
}