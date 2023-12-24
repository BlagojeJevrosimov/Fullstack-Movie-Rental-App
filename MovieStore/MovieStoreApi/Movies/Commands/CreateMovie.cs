using MediatR;
using MovieStore.Core;
using MovieStore.Core.Entities;
using MovieStore.Core.ValueObjects;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Movies.Commands
{
    public static class CreateMovie
    {
        public class Command : IRequest
        {
            public string Name { get; set; } = string.Empty;
            public DateTime? DateOfRelease { get; set; }
            public LicensingTypes LicensingType { get; set; }
        }

        public class CrateMovieRequestHandler : IRequestHandler<Command>
        {
            public readonly IRepository<Movie> _repository;

            public CrateMovieRequestHandler(IRepository<Movie> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task Handle(Command request, CancellationToken cancellationToken)
            {
                ArgumentNullException.ThrowIfNull(request);

                switch (request.LicensingType)
                {
                    case LicensingTypes.LifeLong:
                        {
                            _repository.Add(new LifeLongMovie
                            {
                                Name = request.Name,
                                DateOfRelease = request.DateOfRelease,
                                Price = Money.Create(0).Value
                            });
                            break;
                        }
                    case LicensingTypes.TwoDay:
                        {
                            _repository.Add(new TwoDayMovie
                            {
                                Name = request.Name,
                                DateOfRelease = request.DateOfRelease,
                                Price = Money.Create(0).Value
                            }); 
                            break;
                        }
                    default: throw new ArgumentException();
                }

                _repository.SaveChanges();

                return Task.CompletedTask;
            }
        }
    }
}
