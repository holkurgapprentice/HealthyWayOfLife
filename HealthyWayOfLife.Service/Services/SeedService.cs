using System.Threading.Tasks;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthyWayOfLife.Service.Services
{
    public class SeedService
    {
        private readonly ISeedRepository _seedRepository;
        private readonly TransactionService<HealthyWayOfLifeDbContext> _transactionService;

        public SeedService(ISeedRepository seedRepository, TransactionService<HealthyWayOfLifeDbContext> transactionService)
        {
            _seedRepository = seedRepository;
            _transactionService = transactionService;
        }
        public async Task StartSeed()
        {
            using (var transaction = _transactionService.BeginTransaction())
            {
                await _seedRepository.Seed();
                await transaction.Commit();
            }
        }
    }
}