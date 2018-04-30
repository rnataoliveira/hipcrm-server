using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using server.Data;
using server.Models;
using server.Shared;

namespace server.Controllers.Features.SalesPipelines
{
    public class Create
    {
        public class Command : IRequest<CommandResult<Guid>>
        {
            [Required]
            public Guid? CustomerId { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, CommandResult<Guid>>
        {
            readonly IMediator _mediator;
            readonly ApplicationDbContext _dbContext;

            public Handler(IMediator mediator, ApplicationDbContext dbContext)
            {
                _mediator = mediator;
                _dbContext = dbContext;
            }

            protected async override Task<CommandResult<Guid>> Handle(Command createSale)
            {
                var customer = await _dbContext.Customers.FindAsync(createSale.CustomerId);
                if (customer == null)
                    return CommandResult<Guid>.Fail($"Customer not found with Id: {createSale.CustomerId}");

                var salePipeline = new SalePipeline(customer);

                await _dbContext.AddAsync(salePipeline);
                await _dbContext.SaveChangesAsync();

                // await _mediator.Publish(new Created() { SaleId = salePipeline.Id });
                return CommandResult<Guid>.Success(salePipeline.Id);
            }
        }

        public class Created : INotification
        {
            public Guid SaleId { get; set; }
        }
    }
}