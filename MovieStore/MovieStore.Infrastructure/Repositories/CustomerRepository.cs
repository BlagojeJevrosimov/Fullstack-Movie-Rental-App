using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;

namespace MovieStore.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(MovieStoreContext context) : base(context)
        {
        }
        public override IList<Customer> GetAll() => _context.Customers.Include(c => c.PurchasedMovies).ThenInclude(pm => pm.Movie).ToList();
    }
}
