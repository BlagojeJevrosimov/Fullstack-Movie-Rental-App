using FakeItEasy;
using FluentAssertions;
using MovieStore.Api.Customers.Commands;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStoreApiTests
{
    public class PurchaseMovieTests
    {
        private IRepository<Movie> _movieRepoFake;
        private IRepository<Customer> _customerRepoFake;
        private PurchaseMovie.PurchaseMovieRequestHandler _handler;

        [SetUp]
        public void Setup()
        {

            _movieRepoFake = A.Fake<IRepository<Movie>>();
            _customerRepoFake = A.Fake<IRepository<Customer>>();

            _handler = new PurchaseMovie.PurchaseMovieRequestHandler(_customerRepoFake, _movieRepoFake);
        }

        [Test]
        public void NullRequest_ShouldThrowArgumentNullException()
        {
            Action action = () => _handler.Handle(null!, CancellationToken.None);
            action.Should().ThrowExactly<ArgumentNullException>();
        }
        [Test]
        public void InvalidMovieId_ShouldReturnFalse()
        {
            var command = new PurchaseMovie.Command { MovieId = Guid.NewGuid() };

            A.CallTo(() => _movieRepoFake.GetById(command.MovieId)).Returns(null);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(false);
        }
        [Test]
        public void InvalidCustomerId_ShouldReturnFalse()
        {
            var command = new PurchaseMovie.Command { MovieId = Guid.NewGuid(), CustomerId = Guid.NewGuid() };

            A.CallTo(() => _customerRepoFake.GetById(command.CustomerId)).Returns(null);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(false);
        }
        [Test]
        public void CustomerAlreadyPurchasedMovieWithLifeLongLicense_ShouldReturnFalse()
        {
            var command = new PurchaseMovie.Command { MovieId = Guid.NewGuid(), CustomerId = Guid.NewGuid() };

            var movie = new Movie
            {
                LicensingType = MovieStore.Core.LicensingTypes.LifeLong
            };
            var pm = new PurchasedMovie
            {
                Movie = movie
            };
            List<PurchasedMovie> pmList = new() { pm };
            var customer = new Customer
            {
                PurchasedMovies = pmList
            };

            A.CallTo(() => _customerRepoFake.GetById(command.CustomerId)).Returns(customer);
            A.CallTo(() => _movieRepoFake.GetById(command.MovieId)).Returns(movie);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(false);
        }
        [Test]
        public void SameMoviePurchaseWithTwoDayLicenseNonExpired_ShouldReturnFalse()
        {
            var command = new PurchaseMovie.Command { MovieId = Guid.NewGuid(), CustomerId = Guid.NewGuid() };

            var movie = new Movie
            {
                LicensingType = MovieStore.Core.LicensingTypes.TwoDay
            };
            var pm = new PurchasedMovie
            {
                Movie = movie,
                ExpirationDate = DateTime.UtcNow.AddDays(1)
            };
            List<PurchasedMovie> pmList = new() { pm };
            var customer = new Customer
            {
                PurchasedMovies = pmList
            };

            A.CallTo(() => _customerRepoFake.GetById(command.CustomerId)).Returns(customer);
            A.CallTo(() => _movieRepoFake.GetById(command.MovieId)).Returns(movie);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(false);
        }
        [Test]
        public void SameMoviePurchaseWithTwoDayLicenseExpired_ShouldReturnTrue()
        {
            var command = new PurchaseMovie.Command { MovieId = Guid.NewGuid(), CustomerId = Guid.NewGuid() };

            var movie = new Movie
            {
                LicensingType = MovieStore.Core.LicensingTypes.TwoDay
            };
            var pm = new PurchasedMovie
            {
                Movie = movie,
                ExpirationDate = DateTime.UtcNow.AddDays(-1)
            };
            List<PurchasedMovie> pmList = new() { pm };
            var customer = new Customer
            {
                PurchasedMovies = pmList
            };

            A.CallTo(() => _customerRepoFake.GetById(command.CustomerId)).Returns(customer);
            A.CallTo(() => _movieRepoFake.GetById(command.MovieId)).Returns(movie);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(true);
        }
    }
}
