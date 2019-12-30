using System.Collections.Generic;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model.Database;
using Microsoft.EntityFrameworkCore;

namespace HealthyWayOfLife.Repository.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly HealthyWayOfLifeDbContext _context;

        public ConfigurationRepository(HealthyWayOfLifeDbContext context)
        {
            _context = context;
        }

        public Task<List<Configuration>> GetConfigurationList()
        {
            return _context.Configurations.ToListAsync();
        }

        public Task<Configuration> GetConfiguration(ConfigType configType)
        {
            return _context.Configurations.FirstOrDefaultAsync(x => x.ConfigType == configType);
        }

        public void InsertConfiguration(Configuration configuration)
        {
            _context.Configurations.Add(configuration);
        }
    }
}