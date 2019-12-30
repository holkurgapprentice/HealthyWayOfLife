using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthyWayOfLife.Service.Services
{
    public class TransactionService<TContext> where TContext : DbContext
    {
        private TContext _context;

        public TransactionService(TContext context)
        {
            _context = context;
        }

        public Transaction BeginTransaction()
        {
            return new Transaction(_context);
        }
    }
    public class Transaction : IDisposable
    {
        private readonly IDbContextTransaction _dbContextTransaction;
        private readonly DbContext _context;

        public IDbTransaction GetTransaction()
        {
            return _dbContextTransaction.GetDbTransaction();
        }

        public Transaction(DbContext context)
        {
            _dbContextTransaction = context.Database.BeginTransaction();
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
            _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }

        public void Dispose()
        {
            _dbContextTransaction?.Dispose();
        }
    }
}