using MediatR;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Customers.Commands
{
    public static class DeleteCustomerById
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        public class DeleteCustomerByIdRequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Customer> _repository;

            public DeleteCustomerByIdRequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                ArgumentNullException.ThrowIfNull(request);

                var customer = _repository.GetById(request.Id);

                if (customer is null)
                {
                    return Task.FromResult(false);
                }

                _repository.Remove(customer);
                _repository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
