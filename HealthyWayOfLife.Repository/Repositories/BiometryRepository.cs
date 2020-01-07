using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Models;
using HealthyWayOfLife.Model.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace HealthyWayOfLife.Repository.Repositories
{
    public class BiometryRepository : IBiometryRepository
    {
        private readonly HealthyWayOfLifeDbContext _context;

        public BiometryRepository(HealthyWayOfLifeDbContext context)
        {
            _context = context;
        }
        public Task<List<Biometry>> GetBiometryForUser(User user)
        {
            if (user == null || user.Id == 0)
                throw new HwolException("Invalid data for gathering user biometry, user is null or is invalid");

            return _context.Biometry.Where(b => b.User.Id == user.Id).ToListAsync();
        }

        public async Task<Biometry> AddBiometryForUser(Biometry biometry)
        {
            _context.Biometry.Add(biometry);
            await _context.SaveChangesAsync();
            return _context.Biometry.FirstOrDefaultAsync(b => b.Id == biometry.Id).Result;
        }
    }
}
