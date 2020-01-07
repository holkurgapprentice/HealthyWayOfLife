using System;

namespace HealthyWayOfLife.Model.Models
{
    public interface IScope<out T> : IDisposable
    {
        T Instance { get; }
    }
}