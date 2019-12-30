using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model;
using HealthyWayOfLife.Model.Model.Database;
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
                throw new CustomCodeException("Invalid data for gathering user biometry, user is null or is invalid");

            return _context.Biometry.Where(b => b.User.Id == user.Id).ToListAsync();
        }
    }
}
