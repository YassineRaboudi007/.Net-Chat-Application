using System.Linq.Expressions;

namespace ChatApplication.Domain.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity?> Get(string id);
        Task<List<TEntity>> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Add(TEntity entity);
        bool Remove(TEntity entity);

    }
}
