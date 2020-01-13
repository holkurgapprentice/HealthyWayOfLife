using System.Collections.Generic;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Models.Database;

namespace HealthyWayOfLife.Model.Interfaces
{
    public interface IBiometryRepository
    {
        Task<List<Biometry>> GetBiometryForUser(User user);
        Task<Biometry> GetBiometryForUserWithId(User user, int id);
        Task<Biometry> AddBiometryForUser(Biometry biometry);
        Task<int> ArchiveBiometryForUser(User user, int id);
        Task<Biometry> UpdateBiometryForUser(Biometry biometry);
    }
}
