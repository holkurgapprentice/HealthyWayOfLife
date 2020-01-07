using System;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Models;
using HealthyWayOfLife.Model.Models.Database;
using HealthyWayOfLife.Repository;

namespace HealthyWayOfLife.Service.Services
{
    public class ExceptionService : IExceptionService
    {
        private readonly ILogRepository<HealthyWayOfLifeDbContext> _logRepository;

        public ExceptionService(ILogRepository<HealthyWayOfLifeDbContext> logRepository)
        {
            _logRepository = logRepository;
        }

        public virtual async void HandleException(Exception exception)
        {
            Log log = new Log()
            {
                Exception = exception.Message,
                InnerException = exception.InnerException?.Message,
                LogText = null,
                Stack = exception.StackTrace,
                InsertBy = 1,
                InsertDate = DateTime.Now,
                LogType = LogType.Error,
                UpdateBy = 1,
                UpdateDate = DateTime.Now
            };

            if (exception is HwolException customCodeException)
            {
                log.LogText = $"UserInfo:/{customCodeException.UserInfo}/ ExceptionInfo:/{customCodeException.InnerException}/";
                log.LogType = customCodeException.LogType;
            }


            await _logRepository.InsertLog(log).ConfigureAwait(false);
        }
    }
}