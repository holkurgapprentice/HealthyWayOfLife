using System;
using Autofac.Features.OwnedInstances;
using HealthyWayOfLife.Model.Model;

namespace HealthyWayOfLife.Service.Services
{
    public class Scope<T> : IScope<T>
    {
        private readonly Owned<T> _ownedInstance;

        public Scope(Owned<T> ownedInstance)
        {
            _ownedInstance = ownedInstance;
        }

        public T Instance => _ownedInstance.Value;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ownedInstance?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}