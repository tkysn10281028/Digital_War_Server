namespace ApiServer.Project.Base
{
    public abstract class ApiServerServiceBase
    {
        protected readonly AppDbContext _db;

        public ApiServerServiceBase(AppDbContext db)
        {
            _db = db;
        }

        protected async Task<TResult> ExecuteValidationAsync<TResult>(Func<Task<TResult>> action)
        {
            try
            {
                return await action();
            }
            catch
            {
                throw;
            }
        }

        protected async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> action)
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
    }
}