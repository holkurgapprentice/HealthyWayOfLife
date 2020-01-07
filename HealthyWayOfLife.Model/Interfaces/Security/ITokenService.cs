using System.Collections.Generic;
using System.Security.Claims;
using HealthyWayOfLife.Model.Models.Database;

namespace HealthyWayOfLife.Model.Interfaces.Security
{
    public interface ITokenService
    {
        IEnumerable<Claim> GetClaimsFromToken(string token);
        string GenerateTokenStringForUser(User user);

        string GetTokenStringSignature(string token);
        //bool ValidateTokenString(string token); just check with Db
    }
}