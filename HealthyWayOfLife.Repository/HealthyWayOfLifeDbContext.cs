using System;
using System.Threading;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Models.Database;
using HealthyWayOfLife.Model.Models.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace HealthyWayOfLife.Repository
{
    public class HealthyWayOfLifeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Biometry> Biometry { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Configuration> Configurations { get; set; }

        public HealthyWayOfLifeDbContext(DbContextOptions<HealthyWayOfLifeDbContext> options)
            : base(options)
        {
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAudit();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateAudit()
        {
            foreach (var entityEntry in ChangeTracker.Entries<BaseDatabaseFieldInfo>())
            {
                if (entityEntry.State == EntityState.Added)
                    entityEntry.Entity.InsertDate = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Modified || entityEntry.State == EntityState.Added)
                    entityEntry.Entity.UpdateDate = DateTime.UtcNow;
            }
        }
    }
}