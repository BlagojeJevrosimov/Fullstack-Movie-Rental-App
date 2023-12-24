using MediatR;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Customers.Commands
{
    public static class PromoteCustomer
    {
        public class Command : IRequest<bool>
        {
            public Guid CustomerId { get; set; }
        }

        public class PromoteCustomerRequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Customer> _repository;

            public PromoteCustomerRequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                ArgumentNullException.ThrowIfNull(request);

                var customer = _repository.GetById(request.CustomerId);
                if (customer is null)
                    return Task.FromResult(false);

                if (!customer.Status.IsAdvanced())
                {
                    return Task.FromResult(false);
                }

                if (customer.PurchasedMovies.Count(pm => pm.Movie?.GetType() == typeof(LifeLongMovie) ||
                    (pm.MovieExpirationDate.HasValue && pm.MovieExpirationDate.Value > DateTime.Now)) <= 3)
                {
                    return Task.FromResult(false);
                }

                if (customer.MoneySpent.Value < 200)
                {
                    return Task.FromResult(false);
                }


                customer.PromoteCustomer();
                _repository.SaveChanges();
                return Task.FromResult(true);
            }
        }
    }
}
