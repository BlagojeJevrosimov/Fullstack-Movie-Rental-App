using System.Linq.Expressions;

namespace MovieStore.Infrastructure.Contracts
{
    public interface IRepository<T> where T : class
    {
        T? GetById(Guid id);
        IList<T> GetAll();
        IList<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Remove(T entity);
        void SaveChanges();
        IList<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}
