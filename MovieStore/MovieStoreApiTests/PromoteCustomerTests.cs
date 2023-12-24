using FakeItEasy;
using FluentAssertions;
using MovieStore.Api.Customers.Commands;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStoreApiTests
{
    public class PromoteCustomerTests
    {
        private IRepository<Customer> _customerRepository;
        private PromoteCustomer.PromoteCustomerRequestHandler _handler;

        [SetUp]
        public void Setup()
        {

            _customerRepository = A.Fake<IRepository<Customer>>();
            _handler = new PromoteCustomer.PromoteCustomerRequestHandler(_customerRepository);
        }

        [Test]
        public void NullRequest_ShouldThrowException()
        {
            Action action = () => _handler.Handle(null!, CancellationToken.None);
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void InvalidCustomerId_ShouldReturnFalse()
        {
            var command = new PromoteCustomer.Command { CustomerId = Guid.NewGuid() };

            A.CallTo(() => _customerRepository.GetById(command.CustomerId)).Returns(null);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(false);
        }

        [Test]
        public void CustomerStatusAdvanced_ShouldReturnFalse()
        {
            var command = new PromoteCustomer.Command { CustomerId = Guid.NewGuid() };

            var customer = new Customer();
            customer.Status = MovieStore.Core.Status.Advanced;

            A.CallTo(() => _customerRepository.GetById(command.CustomerId)).Returns(customer);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(false);
        }

        //todo
        [Test]
        public void CustomerStatusAdvancedAndExpired_ShouldReturnFalse()
        {
            var command = new PromoteCustomer.Command { CustomerId = Guid.NewGuid() };

            var customer = new Customer
            {
                Status = MovieStore.Core.Status.Advanced,
                StatusExpirationDate = DateTime.Now.AddDays(-1)
            };
            A.CallTo(() => _customerRepository.GetById(command.CustomerId)).Returns(customer);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(false);
        }

        [Test]
        public void PurchasedMovieCountWithTwoDayLicensesLessThanThree_ShouldReturnFalse()
        {
            var command = new PromoteCustomer.Command { CustomerId = Guid.NewGuid() };

            List<PurchasedMovie> pmList = new();
            var movie = new Movie
            {
                LicensingType = MovieStore.Core.LicensingTypes.TwoDay
            };
            var pm = new PurchasedMovie
            {
                Movie = movie,
                ExpirationDate = DateTime.Now.AddDays(1)
            };
            pmList.Add(pm);
            pmList.Add(pm);
            pm.ExpirationDate = DateTime.Now.AddDays(-2);
            pmList.Add(pm);
            pmList.Add(pm);
            var customer = new Customer
            {
                PurchasedMovies = pmList
            };

            A.CallTo(() => _customerRepository.GetById(command.CustomerId)).Returns(customer);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(false);
        }

        [Test]
        public void PurchasedMovieCountWithLifelongLicenseLessThanThree_ShouldReturnFalse()
        {
            var command = new PromoteCustomer.Command { CustomerId = Guid.NewGuid() };

            List<PurchasedMovie> pmList = A.Fake<List<PurchasedMovie>>();
            var pm = A.Fake<PurchasedMovie>();
            var movie = A.Fake<Movie>();
            movie.LicensingType = MovieStore.Core.LicensingTypes.LifeLong;
            pm.Movie = movie;
            pmList.Add(pm);
            pmList.Add(pm);
            var customer = new Customer();
            customer.PurchasedMovies = pmList;

            A.CallTo(() => _customerRepository.GetById(command.CustomerId)).Returns(customer);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(false);
        }

        [Test]
        public void PurchasedMovieCountWithLifelongLicenseAboveThree_ShouldReturnTrue()
        {
            PromoteCustomer.Command command = new PromoteCustomer.Command { CustomerId = Guid.NewGuid() };

            List<PurchasedMovie> pmList = A.Fake<List<PurchasedMovie>>();
            var pm = A.Fake<PurchasedMovie>();
            var movie = A.Fake<Movie>();
            movie.LicensingType = MovieStore.Core.LicensingTypes.LifeLong;
            pm.Movie = movie;
            pmList.Add(pm);
            pmList.Add(pm);
            pmList.Add(pm);
            pmList.Add(pm);
            var customer = new Customer();
            customer.PurchasedMovies = pmList;

            A.CallTo(() => _customerRepository.GetById(command.CustomerId)).Returns(customer);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(true);
        }

        [Test]
        public void PurchasedMovieCountWithTwoDayLicenseesAboveThree_ShouldReturnTrue()
        {
            PromoteCustomer.Command command = new PromoteCustomer.Command { CustomerId = Guid.NewGuid() };

            List<PurchasedMovie> pmList = A.Fake<List<PurchasedMovie>>();
            var pm = A.Fake<PurchasedMovie>();
            var movie = A.Fake<Movie>();
            movie.LicensingType = MovieStore.Core.LicensingTypes.TwoDay;
            pm.Movie = movie;
            pm.ExpirationDate = DateTime.Now.AddDays(1);
            pmList.Add(pm);
            pmList.Add(pm);
            pmList.Add(pm);
            pmList.Add(pm);
            var customer = new Customer();
            customer.PurchasedMovies = pmList;

            A.CallTo(() => _customerRepository.GetById(command.CustomerId)).Returns(customer);

            var result = _handler.Handle(command, CancellationToken.None);

            result.Result.Should().Be(true);
        }
    }
}
