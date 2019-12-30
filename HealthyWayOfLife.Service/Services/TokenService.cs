using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model.Database;
using HealthyWayOfLife.Repository;
using HealthyWayOfLife.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace HealthyWayOfLife.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly GlobalConfig _globalConfig;
        private readonly ILogRepository<HealthyWayOfLifeDbContext> _logRepository;

        private int SessionTimeMinutes =>
            _globalConfig.SessionTimeMinutes;
        private SymmetricSecurityKey Key => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtSignKey"]));
        private SigningCredentials Credentials => new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        private string Domain => _configuration.GetSection("WebsiteSettings")["Domain"] ?? string.Empty;

        public TokenService(IConfiguration configuration, GlobalConfig globalConfig, ILogRepository<HealthyWayOfLifeDbContext> logRepository)
        {
            _configuration = configuration;
            _globalConfig = globalConfig;
            _logRepository = logRepository;
        }

        public IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            if (ValidateToken(token))
                return (new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken)?.Claims;
            else
                throw new Exception("Token validation error, cannot read claims");
        }

        private bool ValidateToken(string authToken)
        {
            try
            {
                new JwtSecurityTokenHandler().ValidateToken(authToken, GetValidationParameters(), out SecurityToken validatedToken);
                return true;
            }
            catch (Exception e)
            {
                _logRepository.InsertLog("Validate token error", e);
                return false;
            }
        }

        public string GenerateTokenStringForUser(User user) =>
            new JwtSecurityTokenHandler().WriteToken(GetJwtSecurityToken(user.Login));

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidIssuer = Domain,
                ValidAudience = Domain,
                IssuerSigningKey = Key
            };
        }

        private JwtSecurityToken GetJwtSecurityToken(string userName)
        {
            return new JwtSecurityToken(
                issuer: Domain,
                audience: Domain,
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString())
                },
                expires: DateTime.UtcNow.AddMinutes(SessionTimeMinutes),
                signingCredentials: Credentials);
        }
    }
}