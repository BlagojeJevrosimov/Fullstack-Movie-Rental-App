using Microsoft.EntityFrameworkCore;
using MovieStore.Infrastructure.Contracts;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MovieStore.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly MovieStoreContext _context;
        public GenericRepository(MovieStoreContext context)
        {
            _context = context;
        }
        public void Add(T entity) => _context.Set<T>().Add(entity);

        public IList<T> Find(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression).ToList<T>();

        public virtual IList<T> GetAll() => _context.Set<T>().ToList();

        public T? GetById(Guid id) => _context.Set<T>().Find(id)!;

        public void Remove(T entity) => _context.Remove(entity);

        public void SaveChanges() => _context.SaveChanges();
        public IList<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query.Where(predicate).ToList();
        }
    }
}
