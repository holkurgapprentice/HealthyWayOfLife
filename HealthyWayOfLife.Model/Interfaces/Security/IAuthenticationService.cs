using System.Threading.Tasks;
using HealthyWayOfLife.Model.Models.Database;
using HealthyWayOfLife.Model.Models.Dtos;

namespace HealthyWayOfLife.Model.Interfaces.Security
{
    public interface IAuthenticationService
    {
        Task<LoginRequestDto> Login(LoginRequestDto request);
    }
}