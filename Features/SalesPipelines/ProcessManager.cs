using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace server.Features.SalesPipelines
{
    public class ProcessManager : INotificationHandler<Create.Created>
    {
        readonly IMediator _mediator;
        public ProcessManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(Create.Created notification, CancellationToken cancellationToken)
        {
            var createSaleCalendar = new CreateSaleCalendar.Command() { SaleId = notification.SaleId };
            await _mediator.Send(createSaleCalendar);
        }
    }
}