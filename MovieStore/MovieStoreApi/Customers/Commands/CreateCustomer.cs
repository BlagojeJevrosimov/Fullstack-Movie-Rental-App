using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;
using MovieStore.Core;
using MovieStore.Core.Entities;
using MovieStore.Core.ValueObjects;
using MovieStore.Infrastructure.Contracts;

namespace MovieStore.Api.Customers.Commands;

public static class CreateCustomer
{
    public class Command : IRequest<Customer>
    {
        public string Email { get; set; } = string.Empty;
    }

    public class CreateCustomerRequestHandler : IRequestHandler<Command, Customer>
    {
        private readonly IRepository<Customer> _repository;

        public CreateCustomerRequestHandler(IRepository<Customer> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<Customer> Handle(Command request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.Email);
            var customer = _repository.Find(c => c.Email == Email.Create(request.Email).Value).FirstOrDefault();
            if (customer is null)
            {
                customer = new Customer(Email.Create(request.Email).Value);
                
                _repository.Add(customer);
                _repository.SaveChanges();
            }

            return Task.FromResult(customer);
        }
    }
}
