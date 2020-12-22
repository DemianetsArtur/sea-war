using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Social_Network.API.Models.UsersInfo;
using Social_Network.BLL.ModelsDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Social_Network.API.Infrastructure.Manages.JwtManage
{
    public static class JwtManage
    {
        public static string GenerateJwt(UserAccountDto entity, IConfiguration configuration)
        {
            var securityKeyStr = configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeyStr));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var expires = DateTime.Now.AddMinutes(30);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, entity.Email),
                new Claim(ClaimsInfo.FirstName, entity.Name),
                new Claim(ClaimsInfo.Role, entity.UserType),
                new Claim(ClaimsInfo.LastName, entity.LastName),
                new Claim(ClaimsInfo.AboutMe, entity.AboutMe),
                new Claim(ClaimsInfo.Date, entity.Date),
                new Claim(ClaimsInfo.ImagePath, entity.ImagePath),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             claims: claims,
                                             expires: expires,
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
