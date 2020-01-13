using System.Collections.Generic;
using System.Security.Claims;
using HealthyWayOfLife.Model.Models.Database;

namespace HealthyWayOfLife.Model.Interfaces.Security
{
    public interface ITokenService
    {
        IEnumerable<Claim> GetClaimsFromToken(string token);
        string GenerateTokenStringForUser(User user);
    }
}