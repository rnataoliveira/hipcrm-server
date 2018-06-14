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
    public class GetAgreements
    {
        public class Query : IRequest<IEnumerable<Models.Agreement>>
        {
            public string Q { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, IEnumerable<Models.Agreement>>
        {
            readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            protected override async Task<IEnumerable<Models.Agreement>> Handle(Query request)
            {
                IEnumerable<Models.Agreement> agreements = await _context.Agreements
                    .Include(a => a.Sale)
                    .ThenInclude(c => c.Customer)
                    .ThenInclude(c => c.PersonalData)
                    .Include(a => a.PersonalData)
                    .Where(a => a.Sale.Stage != SaleStage.Proposal)
                    .ToListAsync();

                return agreements;
            }
        }
    }
}