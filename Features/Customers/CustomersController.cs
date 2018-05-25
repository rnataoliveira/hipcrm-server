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
        public async Task<IActionResult> GetById([FromRoute]Get.Query query)
        {
            var customer = await _mediator.Send(query);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }
    }
}