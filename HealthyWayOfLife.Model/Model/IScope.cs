using System;

namespace HealthyWayOfLife.Model.Model
{
    public interface IScope<out T> : IDisposable
    {
        T Instance { get; }
    }
}