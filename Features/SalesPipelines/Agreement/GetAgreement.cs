using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using server.Models;
using server.Data;
using server.Shared;
using Microsoft.EntityFrameworkCore;

namespace server.Features.SalesPipelines.Agreement
{
    public class GetAgreement
    {
        public class Query : IRequest<CommandResult<Models.Agreement>>
        {
            [Required]
            public Guid? AgreementId { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, CommandResult<Models.Agreement>>
        {
            readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;                
            }

            protected override async Task<CommandResult<Models.Agreement>> Handle(Query request)
            {
                Models.Agreement agreement = await _context.Agreements
                    .Include(a => a.Sale)
                    .ThenInclude(s => s.Customer)
                    .ThenInclude(c => c.PersonalData)
                    .Include(a => a.PersonalData)
                    .ThenInclude(a => (a as LegalPersonAgreement).Beneficiaries)
                    // .ThenInclude(a => (a as PhysicalPersonAgreement))
                    .FirstOrDefaultAsync(a => a.Id == request.AgreementId);

                if(agreement == null)
                    return CommandResult<Models.Agreement>.Fail("Agreement not found");

                return CommandResult<Models.Agreement>.Success(agreement);
            }
        }
    }
}