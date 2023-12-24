using MediatR;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Movies.Queries;

public static class GetMoviesPagination
{
    public class Query : IRequest<IList<Movie>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
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
            if (request.PageNumber < 0 || request.PageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("Page number and page size can't be less than 1.");
            }

            //maksimalan broj stranica frontu poslati
            var movies = _repository.GetAll();
            movies = movies.Skip(request.PageNumber * request.PageSize).Take(request.PageSize).ToList();

            return Task.FromResult(movies);
        }
    }
}
