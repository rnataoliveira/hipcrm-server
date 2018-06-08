using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using server.Shared;
using HashidsNet;

namespace server.Features.SalesPipelines.Agreement
{
    public class SavaAgreementPhysicalPerson
    {
        public class Command : IRequest<CommandResult<Guid>>
        {
            string _accessToken;

            [Required]
            public Guid? SaleId { get; set; }

            [Required]
            public Guid? AgreementId { get; set; }

            [Required]
            public string AccessToken
            {
                get => _accessToken;
                set => _accessToken = $"Bearer {value}";
            }
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

            protected async override Task<CommandResult<Guid>> Handle(Command saveAgreementPhysicalPerson)
            {
                throw new NotImplementedException();
            }
        }
    }
}