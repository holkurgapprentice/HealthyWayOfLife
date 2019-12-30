using System.Collections.Generic;
using System.Security.Claims;
using HealthyWayOfLife.Model.Model.Database;

namespace HealthyWayOfLife.Model.Interfaces
{
    public interface ITokenService
    {
        IEnumerable<Claim> GetClaimsFromToken(string token);
        string GenerateTokenStringForUser(User user);
        //bool ValidateTokenString(string token); just check with Db
    }
}