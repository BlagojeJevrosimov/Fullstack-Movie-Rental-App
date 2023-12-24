using MediatR;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Movies.Queries
{
    public static class GetAllMovies
    {
        public class Query : IRequest<IList<Movie>>
        {
        }

        public class GetAllMoviesRequestHandler : IRequestHandler<Query, IList<Movie>>
        {
            private readonly IRepository<Movie> _repository;

            public GetAllMoviesRequestHandler(IRepository<Movie> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<IList<Movie>> Handle(Query request, CancellationToken cancellationToken)
            {
                ArgumentNullException.ThrowIfNull(request);

                var movies = _repository.GetAll();

                return Task.FromResult(movies);
            }
        }
    }
}
