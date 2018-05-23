using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace server.Features.SalesPipelines
{
    public class ProcessManager : INotificationHandler<Create.Created>
    {
        readonly IMediator _mediator;
        public ProcessManager(IMediator mediator) =>
            _mediator = mediator;

        public async Task Handle(Create.Created notification, CancellationToken cancellationToken) =>
            await _mediator.Send(new CreateSaleCalendar.Command
            {
                SaleId = notification.SaleId,
                AccessToken = notification.AccessToken
            });
    }
}