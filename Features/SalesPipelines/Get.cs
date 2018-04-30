using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using server.Models;
using server.Data;
using server.Shared;
using Microsoft.EntityFrameworkCore;

namespace server.Features.SalesPipelines
{
    public class Get
    {
        public class Query : IRequest<CommandResult<SalePipeline>>
        {
            [Required]
            public Guid? SaleId { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, CommandResult<SalePipeline>>
        {
            readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;                
            }

            protected override async Task<CommandResult<SalePipeline>> Handle(Query request)
            {
                var salePipeline = await _context.SalesPipelines
                    .Include(sale => sale.Customer)
                    .ThenInclude(customer => customer.Person)
                    .FirstOrDefaultAsync(sale => sale.Id == request.SaleId);
                    
                if(salePipeline == null)
                    return CommandResult<SalePipeline>.Fail($"Sale not found with Id: {request.SaleId}");

                return CommandResult<SalePipeline>.Success(salePipeline);
            }
        }
    }
}