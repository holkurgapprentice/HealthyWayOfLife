using HealthyWayOfLife.Model.Model.Database;

namespace HealthyWayOfLife.Model.Interfaces
{
    public interface ISessionInformation
    {
        void SetSession(Session session);
        Session GetSession();
    }
}