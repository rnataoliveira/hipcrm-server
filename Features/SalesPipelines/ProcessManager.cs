using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace server.Features.SalesPipelines
{
    public class ProcessManager :
        INotificationHandler<Create.Created>,
        INotificationHandler<DeleteSale.Deleted>
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

        public async Task Handle(DeleteSale.Deleted notification, CancellationToken cancellationToken)
        {
            if (notification.Sale.CalendarId != null)
                await _mediator.Send(new DeleteSaleCalendar.Command
                {
                    CalendarId = notification.Sale.CalendarId,
                    AccessToken = notification.AccessToken
                });
        }
    }
}