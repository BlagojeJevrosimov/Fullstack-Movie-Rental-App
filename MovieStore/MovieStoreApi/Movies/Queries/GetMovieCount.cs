using MediatR;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Movies.Queries
{
    public static class GetMovieCount
    {
        public class Query : IRequest<int>
        {
        }

        public class GetMovieCountRequestHandler : IRequestHandler<Query, int>
        {
            private readonly IRepository<Movie> _repository;

            public GetMovieCountRequestHandler(IRepository<Movie> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                ArgumentNullException.ThrowIfNull(request);

                var moviesCount = _repository.GetAll().Count;

                return Task.FromResult(moviesCount);
            }
        }
    }
}
