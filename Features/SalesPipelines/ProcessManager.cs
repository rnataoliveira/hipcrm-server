using System.Threading;
using System.Threading.Tasks;
using MediatR;
using server.Features.SalesPipelines.Agreement;
using server.Models;

namespace server.Features.SalesPipelines
{
    public class ProcessManager :
        INotificationHandler<Create.Created>,
        INotificationHandler<DeleteSale.Deleted>,
        INotificationHandler<Agreement.Created>
    {
        readonly IMediator _mediator;
        public ProcessManager(IMediator mediator) =>
            _mediator = mediator;

        public async Task Handle(Create.Created notification, CancellationToken cancellationToken)
        {
            var calendarCreation = _mediator.Send(new CreateSaleCalendar.Command
            {
                SaleId = notification.SaleId,
                AccessToken = notification.AccessToken
            });

            var folderCreation = _mediator.Send(new CreateSaleFolder.Command
            {
                SaleId = notification.SaleId,
                AccessToken = notification.AccessToken
            });

            await Task.WhenAll(calendarCreation, folderCreation);
        }

        public async Task Handle(DeleteSale.Deleted notification, CancellationToken cancellationToken)
        {
            if (notification.Sale.CalendarId != null)
                await _mediator.Send(new DeleteSaleCalendar.Command
                {
                    CalendarId = notification.Sale.CalendarId,
                    AccessToken = notification.AccessToken
                });
        }

        public async Task Handle(Agreement.Created notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateStage.Command
            {
                SaleId = notification.SaleId,
                Stage = SaleStage.Agreement
            });
        }
    }
}