using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using server.Shared;
using server.Models;
using server.Features.SalesPipelines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace server.Features.SalesPipelines
{
    [Route("sales-pipelines")]
    [Authorize]
    public class SalesPipelinesController : Controller
    {
        readonly IMediator _mediator;

        public SalesPipelinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Post(
            [FromBody] Create.Command command, 
            [FromHeader] string accessToken
        )
        {
            ModelState.Clear();

            command.AccessToken = accessToken;

            if (!TryValidateModel(command))
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
            CommandResult<SalePipeline> result = await _mediator.Send(query);

            if(result)
                return Ok(result.Data);

            return NotFound(result.FailureReason);
        }

        [Route("{saleId}/appointments")]
        public async Task<IActionResult> GetAppointments(GetAppointments.Query query) 
        {
            CommandResult result = await _mediator.Send(query);

            if(result)
                return Ok();

            return NotFound();
        }
    }
}