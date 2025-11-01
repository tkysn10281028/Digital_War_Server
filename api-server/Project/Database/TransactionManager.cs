using ApiServer.Project.Common;
using ApiServer.Project.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Project.Database
{
    public class TransactionManager : IInjectable
    {
        private readonly AppDbContext _db;

        public TransactionManager(AppDbContext db)
        {
            _db = db;
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action)
        {
            await using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await action();
                await tx.CommitAsync();
                return result;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            await using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                await action();
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
