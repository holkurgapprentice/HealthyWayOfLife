using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Model.Database;

namespace HealthyWayOfLife.Model.Interfaces
{
    public interface IBiometryRepository
    {
        Task<List<Biometry>> GetBiometryForUser(User user);
        Task<Biometry> AddBiometryForUser(Biometry biometry);
    }
}
