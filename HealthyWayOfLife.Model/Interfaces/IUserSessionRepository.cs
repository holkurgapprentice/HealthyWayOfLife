using System.Threading.Tasks;
using HealthyWayOfLife.Model.Models.Database;

namespace HealthyWayOfLife.Model.Interfaces
{
    public interface IUserSessionRepository
    {
        Task InsertUserSession(Session userSession);
        Task<Session> GetExistingUserSession(string token);
        Task UpdateSessionRefreshInfo(Session session);
        Task<Session> GetExistingUserSessionWithUser(string token);
        Task CloseOpenSessions(int userId);
    }
}