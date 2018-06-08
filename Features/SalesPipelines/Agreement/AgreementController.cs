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
    [Route("sales-pipelines")]
    [Authorize]
    public class AgreementController : Controller
    {
        readonly IMediator _mediator;

        public AgreementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("{saleId}/agreement/legal-person")]
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

            CommandResult<LegalPersonAgreement> result = await _mediator.Send(command);
            if(!result.IsSuccess)
                return BadRequest(result.FailureReason);

            return Created($"/sales-pipelines/{command.SaleId}/agreement", result.Data);
        }

        [Route("physical-person")]
        [HttpPost]
        public async Task<IActionResult> SaveAgreementPhysicalPerson(
            [FromBody] SavaAgreementPhysicalPerson.Command command,
            [FromHeader] string accessToken)
        {
            ModelState.Clear();

            command.AccessToken = accessToken;

            if (!TryValidateModel(command))
                return BadRequest(ModelState);

            CommandResult<Guid> result = await _mediator.Send(command);

            return Created($"/sales-pipelines/{command.SaleId}/agreement", new {});
        }
    }
}