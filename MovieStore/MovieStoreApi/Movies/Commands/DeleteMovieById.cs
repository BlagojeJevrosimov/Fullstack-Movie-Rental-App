using MediatR;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Movies.Commands
{
    public static class DeleteMovieById
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        public class DeleteMovieByIdRequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Movie> _repository;

            public DeleteMovieByIdRequestHandler(IRepository<Movie> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                ArgumentNullException.ThrowIfNull(request);

                var movie = _repository.GetById(request.Id);

                if (movie is null)
                {
                    return Task.FromResult(false);
                }

                _repository.Remove(movie);
                _repository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
