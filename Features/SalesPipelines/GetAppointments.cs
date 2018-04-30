using System.Threading.Tasks;
using MediatR;
using server.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using server.Data;
using server.Models;

namespace server.Features.SalesPipelines
{
    public class GetAppointments
    {
        public class Query : IRequest<CommandResult>
        {
            [Required]
            public Guid? SaleId { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, CommandResult>
        {
            readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            protected override async Task<CommandResult> Handle(Query getAppointments)
            {
                SalePipeline sale = await _context.SalesPipelines.FindAsync(getAppointments.SaleId);

                if(sale == null)
                    return CommandResult.Fail($"Sale not found with Id: {getAppointments.SaleId}");

                return CommandResult.Success;
            }
        }
    }
}