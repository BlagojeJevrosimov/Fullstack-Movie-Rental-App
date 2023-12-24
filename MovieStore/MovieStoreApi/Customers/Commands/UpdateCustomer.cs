using MediatR;
using MovieStore.Core;
using MovieStore.Core.Entities;
using MovieStore.Core.ValueObjects;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Customers.Commands
{
    public static class UpdateCustomer
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
            public string Email { get; set; } = string.Empty;
            public Role Role { get; set; }
        }

        public class UpdateCustomerRequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Customer> _repository;

            public UpdateCustomerRequestHandler(IRepository<Customer> repository)
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

                customer.UpdateCustomer(Email.Create(request.Email).Value, request.Role);
                _repository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
