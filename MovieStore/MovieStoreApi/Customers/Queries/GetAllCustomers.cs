using MediatR;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Customers.Queries
{
    public static class GetAllCustomers
    {
        public class Query : IRequest<IList<Customer>>
        {
        }

        public class GetAllCustomersRequestHandler : IRequestHandler<Query, IList<Customer>>
        {
            private readonly IRepository<Customer> _repository;

            public GetAllCustomersRequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<IList<Customer>> Handle(Query request, CancellationToken cancellationToken)
            {
                ArgumentNullException.ThrowIfNull(request);

                var customers = _repository.GetAll();

                return Task.FromResult(customers);
            }
        }
    }
}
