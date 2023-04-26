using ChatApplication.Domain.Abstractions.UnitOfWork;

namespace ChatApplication.Infrastructure.UntiOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Complete()
        {
            try
            {
                int res = await _appDbContext.SaveChangesAsync();
                if (res > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
