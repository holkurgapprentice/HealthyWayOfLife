using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model.Database;
using Microsoft.EntityFrameworkCore;
using ValueType = HealthyWayOfLife.Model.Enums.ValueType;

namespace HealthyWayOfLife.Repository.Repositories
{
    public class SeedRepository : ISeedRepository
    {
        private readonly HealthyWayOfLifeDbContext _dbContext;

        public SeedRepository(HealthyWayOfLifeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Seed()
        {
            int count = await _dbContext.Users.CountAsync();

            if (count != 0)
            {
                return;
            }

            User userAdmin = new User()
            {
                Login = "Admin",
                Password = "21232F297A57A5A743894A0E4A801FC3",
                Email = "admin@hwol.com",
                IsActive = true,
                IsAdmin = true,
            
            }.CompleteDefaultValue<User>();


            _dbContext.Users.Add(userAdmin);
            await _dbContext.SaveChangesAsync();
            
            Configuration sessionTime = new Configuration()
            {
                ConfigType = ConfigType.SessionTime,
                IntValue = 300,
                ValueType = ValueType.Int
            }.CompleteDefaultValue<Configuration>();

            _dbContext.Configurations.Add(sessionTime);
        }
    }
}