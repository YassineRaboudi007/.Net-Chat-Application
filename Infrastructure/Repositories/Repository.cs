using System.Linq.Expressions;
using ChatApplication.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public ValueTask<T?> Get(string id)
        {
            return _appDbContext.Set<T>().FindAsync(Guid.Parse(id));
        }

        public Task<List<T>> GetAll()
        {
            return _appDbContext.Set<T>().ToListAsync();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().Where(predicate);
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                await _appDbContext.Set<T>().AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Remove(T entity)
        {
            try
            {
                _appDbContext.Set<T>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
