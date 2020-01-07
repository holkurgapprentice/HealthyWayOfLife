using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HealthyWayOfLife.Service.Services.Security
{
    public class JwtTokenConfigurationService
    {
        private readonly IConfiguration _configuration;
        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtSignKey"]));
        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        public string Domain => _configuration.GetSection("WebsiteSettings")["Domain"] ?? string.Empty;
        public string Audience => _configuration.GetSection("WebsiteSettings")["Audience"] ?? string.Empty;

        public JwtTokenConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidIssuer = Domain,
                ValidAudience = Audience,
                IssuerSigningKey = SymmetricSecurityKey,
                ClockSkew = TimeSpan.Zero
            };
        }

        public JwtSecurityToken GetJwtSecurityToken(int sessionTimeoutMinutes, IEnumerable<Claim> claims = null)
        {
            return new JwtSecurityToken(
                issuer: Domain,
                audience: Audience,
                expires: DateTime.UtcNow.AddMinutes(sessionTimeoutMinutes),
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials,
                claims: claims);
        }

        //=  claims: new Claim[]
        //{
        //    new Claim(ClaimTypes.Name, userName),
        //    new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString())
        //},
    }
}