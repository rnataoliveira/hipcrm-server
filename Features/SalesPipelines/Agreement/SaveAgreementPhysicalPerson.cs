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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace server.Features.SalesPipelines.Agreement
{
    public class SaveAgreementPhysicalPerson
    {
        public class Command : IRequest<CommandResult<Models.Agreement>>
        {
            string _accessToken;

            [Required]
            public Guid? SaleId { get; set; }

            [Required]
            public string AccessToken
            {
                get => _accessToken;
                set => _accessToken = $"Bearer {value}";
            }

            [Required]
            public string Number { get; set; }

            [Required]
            public string Plan { get; set; }
            
            public string Notes { get; set; }

            [Required]
            public PaymentInfo Payment { get; set; }

            public ICollection<Dependent> Dependents { get; set; } = new Collection<Dependent>();
        }

        public class Handler : AsyncRequestHandler<Command, CommandResult<Models.Agreement>>
        {
            readonly IMediator _mediator;
            readonly ApplicationDbContext _dbContext;

            public Handler(IMediator mediator, ApplicationDbContext dbContext)
            {
                _mediator = mediator;
                _dbContext = dbContext;
            }

            protected async override Task<CommandResult<Models.Agreement>> Handle(Command command)
            {
                SalePipeline sale = _dbContext.SalesPipelines.FirstOrDefault(c => c.Id == command.SaleId);
                if (sale == null)
                    return CommandResult<Models.Agreement>.Fail("Venda não encontrada!");

                if (sale.Stage != SaleStage.Proposal)
                    return CommandResult<Models.Agreement>.Fail("Não é possivel criar um contrato para esta venda!");

                Models.PhysicalPersonAgreement personalData = new Models.PhysicalPersonAgreement 
                {
                    Plan = command.Plan,
                    Dependents = command.Dependents
                };

                Models.Agreement agreement = new Models.Agreement 
                {
                    Sale = sale,
                    Number = command.Number,
                    Notes = command.Notes,
                    Payment = command.Payment,
                    PersonalData = personalData
                };

                await _dbContext.Agreements.AddAsync(agreement);
                await _mediator.Publish(new Created
                {
                    AgreementId = agreement.Id,
                    SaleId = sale.Id,
                    AccessToken = command.AccessToken
                });

                await _dbContext.SaveChangesAsync();

                return CommandResult<Models.Agreement>.Success(agreement);
            }
        }
    }
}