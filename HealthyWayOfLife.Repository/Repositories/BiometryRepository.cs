using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Enums;
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
            return _context.Biometry.Where(b => b.User.Id == user.Id).ToListAsync();
        }

        public Task<Biometry> GetBiometryForUserWithId(User user, int id)
        {
            return _context.Biometry.FirstOrDefaultAsync(b => b.User.Id == user.Id && b.Id == id);
        }

        public async Task<Biometry> AddBiometryForUser(Biometry biometry)
        {
            _context.Biometry.Add(biometry);
            await _context.SaveChangesAsync();
            return _context.Biometry.FirstOrDefaultAsync(b => b.Id == biometry.Id).Result;
        }

        public async Task<Biometry> UpdateBiometryForUser(Biometry biometry)
        {
            var biometryFromDb = await _context.Biometry.FindAsync(biometry.Id);
            
            if (biometryFromDb != null)
            {
                throw new HwolException("Sorry object not found for update");
            }

            _context.Entry(biometryFromDb).CurrentValues.SetValues(biometry);
            await _context.SaveChangesAsync();
            return await _context.Biometry.FindAsync(biometry.Id);
        }

        public async Task<int> ArchiveBiometryForUser(User user, int id)
        {
            var biometries = await _context.Biometry.Where(x => x.User.Id == user.Id && x.Id == id).ToListAsync();
            if (biometries != null)
            {
                biometries[0].IsArchive = 1;
            }

            return await _context.SaveChangesAsync();
        }

    }
}
