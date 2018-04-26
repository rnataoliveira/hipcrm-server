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
            protected override Task<string> Handle(Command request)
            {
                return Task.FromResult(request.CustomerId);
            }
        }
    }
}