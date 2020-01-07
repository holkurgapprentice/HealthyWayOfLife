using System;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Models;
using HealthyWayOfLife.Model.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace HealthyWayOfLife.Repository.Repositories
{
    public class LogRepository<TContext> : ILogRepository<TContext> where TContext : DbContext
    {
        private TContext Context => _contextScope.Instance;

        private readonly IScope<TContext> _contextScope;

        public LogRepository()
        {
        }

        public LogRepository(IScope<TContext> contextScope)
        {
            _contextScope = contextScope;
        }

        public Task InsertLog(Log log)
        {
            Context.Add(log);
            return Context.SaveChangesAsync();
        }

        public async Task InsertLog(LogType logType, string text)
        {
            Log log = new Log()
            {
                LogType = logType,
                LogText = text,
                InsertDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            await InsertLog(log);
        }

        public async Task InsertLog(string text, Exception exception)
        {
            Log log = new Log()
            {
                Exception = exception.ToString(),
                LogType = LogType.Error,
                LogText = text,
                InsertDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            await InsertLog(log);
        }
    }
}