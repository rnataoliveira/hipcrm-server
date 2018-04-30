using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using server.Shared;

namespace server.Controllers.Features.SalesPipelines
{
    [Route("sales-pipelines")]
    public class SalesPipelinesController : Controller
    {
        readonly IMediator _mediator;

        public SalesPipelinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Post([FromBody] Create.Command command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CommandResult<Guid> result = await _mediator.Send(command);

            if (!result)
            {
                ModelState.AddModelError("customerId", result.FailureReason);
                return BadRequest(ModelState);
            }

            return Created($"/sales-pipelines/{result.Data}", new { saleId = result.Data });
        }

        [Route("{saleId}")]
        public async Task<IActionResult> Get(Get.Query query)
        {
            var result = await _mediator.Send(query);

            return Ok(result.Sale);
        }
    }
}