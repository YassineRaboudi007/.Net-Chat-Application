using System.Linq.Expressions;
using ChatApplication.Domain.Abstractions.Repositories;

namespace ChatApplication.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public T Get(string id)
        {
            return _appDbContext.Set<T>().Find(Guid.Parse(id));
        }

        public IEnumerable<T> GetAll()
        {
            return _appDbContext.Set<T>().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().Where(predicate);
        }

        public bool Add(T entity)
        {
            try
            {
                _appDbContext.Set<T>().Add(entity);
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
