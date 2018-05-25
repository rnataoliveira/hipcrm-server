using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using server.Models;
using server.Data;
using server.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace server.Features.SalesPipelines
{
    public class GetSales
    {
        public class Query : IRequest<IEnumerable<SalePipeline>>
        {
            public string Q { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, IEnumerable<SalePipeline>>
        {
            readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;                
            }

            protected override async Task<IEnumerable<SalePipeline>> Handle(Query request)
            {
                IEnumerable<SalePipeline> sales = await _context.SalesPipelines
                    .Include(sale => sale.Customer)
                    .ThenInclude(customer => customer.PersonalData)
                    .Where(sale => (string.IsNullOrEmpty(request.Q) || sale.Code == request.Q))
                    .ToListAsync();

                return sales;
            }
        }
    }
}