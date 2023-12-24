using MovieStore.Core.Entities;

namespace MovieStore.Infrastructure.Repositories
{
    public class MovieRepository : GenericRepository<Movie>
    {
        public MovieRepository(MovieStoreContext context) : base(context)
        {
        }
    }
}
