using MediatR;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Movies.Queries
{
    public static class GetMovieById
    {
        public class Query : IRequest<Movie?>
        {
            public Guid Id { get; set; }
        }

        public class GetMovieByIdRequestHandler : IRequestHandler<Query, Movie?>
        {
            private readonly IRepository<Movie> _repository;

            public GetMovieByIdRequestHandler(IRepository<Movie> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Movie?> Handle(Query request, CancellationToken cancellationToken)
            {
                ArgumentNullException.ThrowIfNull(request);

                var movie = _repository.GetById(request.Id);

                return Task.FromResult(movie);
            }
        }
    }
}
