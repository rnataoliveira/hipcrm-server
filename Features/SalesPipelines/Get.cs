using System.Threading.Tasks;
using MediatR;

namespace server.Controllers.Features.SalesPipelines
{
    public class Get
    {
        public class Query : IRequest<Result>
        {
            public string SaleId { get; set; }
        }

        public class Result
        {
            public SalePipeline Sale { get; set; }

            public class SalePipeline
            {
                public string SaleId { get; set; }
            }
        }

        public class Handler : AsyncRequestHandler<Query, Result>
        {
            protected override Task<Result> Handle(Query request)
            {
                var sale = new Result.SalePipeline() { SaleId = request.SaleId};
                var result = new Result() { Sale = sale };

                return Task.FromResult(result);
            }
        }
    }
}