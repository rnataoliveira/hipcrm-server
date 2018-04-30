using System.Threading.Tasks;
using MediatR;

namespace server.Controllers.Features.SalesPipelines
{
    public class Create
    {
        public class Command : IRequest<string>
        {
            public string CustomerId { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, string>
        {
            readonly IMediator _mediator;

            public Handler(IMediator mediator) => _mediator = mediator;

            protected async override Task<string> Handle(Command createSale)
            {
                
                
                await _mediator.Publish(new Created() { SaleId = createSale.CustomerId });
                return createSale.CustomerId;
            }
        }

        public class Created : INotification 
        { 
            public string SaleId { get; set; }
        }
    }
}