using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Api.Customers.Commands;
using MovieStore.Api.Customers.Queries;
using MovieStore.Core.Entities;

namespace MovieStore.Api.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<Customer>>> GetAllCustomers()
        {

            var customers = await _mediator.Send(new GetAllCustomers.Query());

            if (customers == null || customers.Count == 0)
                return NotFound();

            return Ok(customers);

        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> GetCustomerById([FromRoute] GetCustomerById.Query command)
        {
            var customer = await _mediator.Send(command);

            return customer is null ? (ActionResult<Customer>)NotFound() : (ActionResult<Customer>)Ok(customer);
        }

        // POST api/<CustomersController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Customer>> CreateCustomer()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;

            ArgumentException.ThrowIfNullOrEmpty(email, nameof(email));

            var customer = await _mediator.Send(new CreateCustomer.Command
            {
                Email = email
            });

            return Ok(customer);
        }

        // PUT api/<CustomersController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCustomer(UpdateCustomer.Command command)
        {
            var result = await _mediator.Send(command);

            return result ? Ok() : NotFound();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCustomerById(DeleteCustomerById.Command command)
        {
            var result = await _mediator.Send(command);

            return result ? Ok() : NotFound();
        }

        [HttpPost("purchase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PurchaseMovie(PurchaseMovie.Command command)
        {
            var result = await _mediator.Send(command);

            //TODO add bad request later
            return result ? Ok() : NotFound();
        }

        [HttpPut("promote")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> Promote(PromoteCustomer.Command command)
        {
            var result = await _mediator.Send(command);

            var customer = await _mediator.Send(new GetCustomerById.Query() { Id = command.CustomerId });
            //TODO add bad request later
            return result ? Ok(customer) : NotFound();
        }

    }
}
