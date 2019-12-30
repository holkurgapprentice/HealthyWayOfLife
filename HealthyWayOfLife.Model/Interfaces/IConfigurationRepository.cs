using System.Collections.Generic;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Model.Database;

namespace HealthyWayOfLife.Model.Interfaces
{
    public interface IConfigurationRepository
    {
        Task<List<Configuration>> GetConfigurationList();
        Task<Configuration> GetConfiguration(ConfigType configType);
        void InsertConfiguration(Configuration configuration);
    }
}