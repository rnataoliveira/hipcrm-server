using System;
using System.Threading.Tasks;
using MediatR;
using Google.Apis.Services;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using server.Facades.Google;
using server.Facades.Google.Models;
using server.Data;
using server.Models;
using Refit;
using Newtonsoft.Json.Linq;
using server.Shared;
using System.Linq;

namespace server.Features.SalesPipelines
{
    public class UpdateStage
    {
        public class Command : IRequest
        {
            public Guid SaleId { get; set; }

            public SaleStage Stage { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command> 
        {
            readonly IMediator _mediator;
            readonly ApplicationDbContext _dbContext;

            public Handler(IMediator mediator, ApplicationDbContext dbContext)
            {
                _mediator = mediator;
                _dbContext = dbContext;
            }

            protected override async Task Handle(Command command) 
            {
                SalePipeline sale = _dbContext.SalesPipelines.FirstOrDefault(s => s.Id == command.SaleId);
                sale.Stage = command.Stage;

                _dbContext.SalesPipelines.Update(sale);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}