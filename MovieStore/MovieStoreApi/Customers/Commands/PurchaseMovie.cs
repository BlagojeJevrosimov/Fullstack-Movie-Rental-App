using MediatR;
using MovieStore.Core;
using MovieStore.Core.Entities;
using MovieStore.Core.ValueObjects;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Customers.Commands
{
    public static class PurchaseMovie
    {
        public class Command : IRequest<bool>
        {
            public Guid CustomerId { get; set; }
            public Guid MovieId { get; set; }
        }
        public class PurchaseMovieRequestHandler : IRequestHandler<Command, bool>
        {
            public readonly IRepository<Customer> _customerRepository;
            public readonly IRepository<Movie> _movieRepository;

            public PurchaseMovieRequestHandler(IRepository<Customer> customerRepository, IRepository<Movie> movieRepository)
            {
                _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
                _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                ArgumentNullException.ThrowIfNull(request);

                var movie = _movieRepository.GetById(request.MovieId);

                if (movie is null)
                    return Task.FromResult(false);

                var customer = _customerRepository.GetById(request.CustomerId);

                if (customer is null)
                    return Task.FromResult(false);

                if (customer.PurchasedMovies.Any(pm => pm.Movie == movie && (pm.Movie.GetType() == typeof(LifeLongMovie) ||
                    (pm.MovieExpirationDate.HasValue && pm.MovieExpirationDate.Value > DateTime.Now))))
                {
                    return Task.FromResult(false);
                }

                customer.PurchaseMovie(movie);

                _customerRepository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
