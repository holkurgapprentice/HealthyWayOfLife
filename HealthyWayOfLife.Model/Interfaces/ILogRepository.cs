using System;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Model.Database;

namespace HealthyWayOfLife.Model.Interfaces
{
    public interface ILogRepository<TContext>
    {
        Task InsertLog(Log log);

        Task InsertLog(LogType logType, string text);
        Task InsertLog(string text, Exception exception);
    }
}