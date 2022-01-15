using DeliveryService.Config;
using DeliveryService.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services
{
    public class AccessTokenGenerator
    {
        private readonly AuthConfig authConfig;

        public AccessTokenGenerator(AuthConfig authConfig)
        {
            this.authConfig = authConfig;
        }

        public string GenerateToken(BaseUserModel user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.AccessTokenSecrtet));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                authConfig.Issuer,
                authConfig.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(authConfig.AccessTokenExpirationMinutes),
                credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
