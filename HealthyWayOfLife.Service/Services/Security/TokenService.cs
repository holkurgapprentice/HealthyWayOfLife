using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Interfaces.Security;
using HealthyWayOfLife.Model.Models;
using HealthyWayOfLife.Model.Models.Database;
using HealthyWayOfLife.Repository;
using HealthyWayOfLife.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HealthyWayOfLife.Service.Services.Security
{
    public class TokenService : ITokenService
    {
        private readonly GlobalConfig _globalConfig;
        private readonly ILogRepository<HealthyWayOfLifeDbContext> _logRepository;
        private readonly JwtTokenConfigurationService _jwtTokenConfigurationService;
        private int SessionTimeMinutes => _globalConfig.SessionTimeMinutes;

        public TokenService(IConfiguration configuration, GlobalConfig globalConfig, ILogRepository<HealthyWayOfLifeDbContext> logRepository, JwtTokenConfigurationService jwtTokenConfigurationService)
        {
            _globalConfig = globalConfig;
            _logRepository = logRepository;
            _jwtTokenConfigurationService = jwtTokenConfigurationService;
        }

        public IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            if (ValidateToken(token))
                return (new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken)?.Claims;
            else
                throw new HwolException("Token validation error, cannot read claims");
        }
        
        private bool ValidateToken(string authToken)
        {
            try
            {
                new JwtSecurityTokenHandler().ValidateToken(authToken, _jwtTokenConfigurationService.GetValidationParameters(), out SecurityToken validatedToken);
                return true;
            }
            catch (Exception e)
            {
                _logRepository.InsertLog("Validate token error", e);
                return false;
            }
        }
        
        public string GenerateTokenStringForUser(User user) =>
            new JwtSecurityTokenHandler().WriteToken(GenerateTokenForUser(user));

        public string GetTokenStringSignature(string token)
        {
            if (!string.IsNullOrEmpty(token) && token.Contains('.'))
            {
                return token.Split('.').Last();
            }
            throw new HwolException("Unsupported token.");
        }

        private SecurityToken GenerateTokenForUser(User user) =>
            _jwtTokenConfigurationService.GetJwtSecurityToken(SessionTimeMinutes, new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username ?? ""),
            });
    }
}