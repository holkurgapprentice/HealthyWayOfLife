using System;

namespace HealthyWayOfLife.Model.Interfaces
{
    public interface IExceptionService
    {
        void HandleException(Exception exception);
    }
}