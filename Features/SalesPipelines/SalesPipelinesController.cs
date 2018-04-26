using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            var saleId = await _mediator.Send(command);

            return Created($"/sales-pipelines/{saleId}", new { saleId });
        }

        [Route("{saleId}")]
        public async Task<IActionResult> Get(Get.Query query) 
        {
            var result = await _mediator.Send(query);

            return Ok(result.Sale);
        }
    }
}