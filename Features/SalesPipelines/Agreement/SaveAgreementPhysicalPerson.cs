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

            public string contractNumber { get; set; }

            public string Plan { get; set; }

            public PhoneNumber phone { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            public string companyContact { get; set; }

            public Address Address { get; set; } //O endereço do contrato possui referencia também

            public Dependents Dependents { get; set; }

            public double Comission { get; set; }

            public double TotalValue { get; set; }

            public double EntranceFee { get; set; }

            public double InstallmentAmount { get; set; }

            public double TotalValue { get; set; }

            public double AmountValue => (this.totalValue - this.entranceFee) / this.installmentAmount;

            public string Notes { get; set; }
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
                return NotImplementedException()
            }
        }
    }
}