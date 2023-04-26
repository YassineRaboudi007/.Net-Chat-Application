using System.Linq.Expressions;

namespace ChatApplication.Domain.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        bool Add(TEntity entity);
        bool Remove(TEntity entity);

    }
}
