using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace server.Controllers.Features.SalesPipelines 
{
    public class ProcessManager : INotificationHandler<Create.Created>
    {
        public Task Handle(Create.Created notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}