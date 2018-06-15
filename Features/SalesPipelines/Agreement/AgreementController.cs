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
using System.Collections.Generic;

namespace server.Features.SalesPipelines.Agreement
{
    [Authorize]
    public class AgreementController : Controller
    {
        readonly IMediator _mediator;

        public AgreementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("agreements/{agreementId}")]
        [HttpGet]
        public async Task<IActionResult> GetAgreement(Agreement.GetAgreement.Query query) 
        {
            CommandResult<Models.Agreement> result = await _mediator.Send(query);

            if(result) return Ok(result.Data);

            return NotFound();
        }

        [Route("agreements")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAgreements.Query query) 
        {
            IEnumerable<Models.Agreement> agreements = await _mediator.Send(query);

            return Ok(agreements);
        }

        [Route("sales-pipelines/{saleId}/agreement/legal-person")]
        [HttpPost]
        public async Task<IActionResult> SaveAgreementLegalPerson(
            [FromBody] SaveAgreementLegalPerson.Command command,
            [FromRoute] Guid saleId,
            [FromHeader] string accessToken)
        {
            ModelState.Clear();

            command.AccessToken = accessToken;
            command.SaleId = saleId;

            if (!TryValidateModel(command))
                return BadRequest(ModelState);

            CommandResult<Models.Agreement> result = await _mediator.Send(command);
            if(!result.IsSuccess)
                return BadRequest(result.FailureReason);

            return Created($"/agreements/{result.Data.Id}", result.Data);
        }

        [Route("sales-pipelines/{saleId}/agreement/physical-person")]
        [HttpPost]
        public async Task<IActionResult> SaveAgreementPhysicalPerson(
            [FromBody] SaveAgreementPhysicalPerson.Command command,
            [FromRoute] Guid saleId,
            [FromHeader] string accessToken)
        {
            ModelState.Clear();

            command.AccessToken = accessToken;
            command.SaleId = saleId;

            if (!TryValidateModel(command))
                return BadRequest(ModelState);

            CommandResult<Models.Agreement> result = await _mediator.Send(command);
            if(!result.IsSuccess)
                return BadRequest(result.FailureReason);

            return Created($"/agreements/{result.Data.Id}", result.Data);
        }
    }
}