using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using server.Shared;
using server.Models;
using server.Features.Customers;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.Collections.Generic;
using server.Features.Customers.Create;
using server.Features.Customers.Update;

namespace server.Features.Customers
{
    [Route("customers")]
    [Authorize]
    public class CustomersController : Controller
    {
        readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("search")]
        public async Task<IActionResult> Search([FromQuery]Search.Query search)
        {
            IEnumerable<Search.CustomerSearchResult> customers = await _mediator.Send(search);

            return Ok(customers);
        }

        public async Task<IActionResult> Get([FromQuery]GetCustomers.Query query)
        {
            IEnumerable<Customer> customers = await _mediator.Send(query);

            return Ok(customers);
        }

        [Route("{customerId}")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute]Get.Query query)
        {
            var customer = await _mediator.Send(query);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [Route("legal-person")]
        [HttpPost]
        public async Task<IActionResult> CreateLegalPerson(
            [FromBody] CreateLegalPersonCustomer.Command command,
            [FromHeader] string accessToken)
        {
            ModelState.Clear();

            command.AccessToken = accessToken;

            if (!TryValidateModel(command))
                return BadRequest(ModelState);

            CommandResult<Guid> result = await _mediator.Send(command);

            if (!result)
            {
                ModelState.AddModelError("companyRegistration", result.FailureReason);
                return BadRequest(ModelState);
            }

            return Created($"/customers/{result.Data}", new { customerId = result.Data });
        }

        [Route("physical-person")]
        [HttpPost]
        public async Task<IActionResult> CreatePhysicalPerson(
            [FromBody] CreatePhysicalPersonCustomer.Command command,
            [FromHeader] string accessToken)
        {
            ModelState.Clear();

            command.AccessToken = accessToken;

            if (!TryValidateModel(command))
                return BadRequest(ModelState);

            CommandResult<Guid> result = await _mediator.Send(command);

            if (!result)
            {
                ModelState.AddModelError("documentNumber", result.FailureReason);
                return BadRequest(ModelState);
            }

            return Created($"/customers/{result.Data}", new { customerId = result.Data });
        }

        [Route("physical-person")]
        [HttpPut]
        public async Task<IActionResult> UpdatePhysicalPerson(
            [FromBody] UpdatePhysicalPersonCustomer.Command command,
            [FromHeader] string accessToken
        ) 
        {
            ModelState.Clear();

            command.AccessToken = accessToken;

            if (!TryValidateModel(command))
                return BadRequest(ModelState);

            CommandResult<Customer> result = await _mediator.Send(command);

            if (!result)
            {
                ModelState.AddModelError("customer", result.FailureReason);
                return BadRequest(ModelState);
            }

            return Ok(result.Data);
        }

        [Route("legal-person")]
        [HttpPut]
        public async Task<IActionResult> UpdateLegalPerson(
            [FromBody] UpdateLegalPersonCustomer.Command command,
            [FromHeader] string accessToken
        ) 
        {
            ModelState.Clear();

            command.AccessToken = accessToken;

            if (!TryValidateModel(command))
                return BadRequest(ModelState);

            CommandResult<Customer> result = await _mediator.Send(command);

            if (!result)
            {
                ModelState.AddModelError("customer", result.FailureReason);
                return BadRequest(ModelState);
            }

            return Ok(result.Data);
        }

        [HttpDelete]
        [Route("{customerId}")]
        public async Task<IActionResult> Delete(
            [FromRoute] DeleteCustomer.Command command, 
            [FromHeader] string accessToken
        )
        {
            ModelState.Clear();

            command.AccessToken = accessToken;

            if (!TryValidateModel(command))
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            if(result.IsSuccess)
                return NoContent();

            return BadRequest();
        }
    }
}