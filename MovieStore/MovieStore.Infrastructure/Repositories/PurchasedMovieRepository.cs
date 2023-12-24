using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Infrastructure.Repositories
{
    public class PurchasedMovieRepository : GenericRepository<PurchasedMovie>, IRepository<PurchasedMovie>
    {
        public PurchasedMovieRepository(MovieStoreContext context) : base(context)
        {
        }
    }
}
