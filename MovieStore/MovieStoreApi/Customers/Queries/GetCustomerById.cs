using MediatR;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Customers.Queries
{
    public static class GetCustomerById
    {
        public class Query : IRequest<Customer?>
        {
            public Guid Id { get; set; }
        }

        public class GetCustomerByIdRequestHandler : IRequestHandler<Query, Customer?>
        {
            private readonly IRepository<Customer> _repository;

            public GetCustomerByIdRequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Customer?> Handle(Query request, CancellationToken cancellationToken)
            {
                ArgumentNullException.ThrowIfNull(request);

                var customer = _repository.GetById(request.Id);

                return Task.FromResult(customer);
            }
        }
    }
}
