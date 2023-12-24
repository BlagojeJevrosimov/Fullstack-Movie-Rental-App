using MediatR;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Movies.Commands
{
    public static class UpdateMovie
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime? DateOfRelease { get; set; }
        }

        public class UpdateCustomerRequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Movie> _repository;

            public UpdateCustomerRequestHandler(IRepository<Movie> repository)
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

                movie.DateOfRelease = request.DateOfRelease;
                movie.Name = request.Name;

                _repository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
