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
    public class SaveAgreementLegalPerson
    {
        public class Command : IRequest<CommandResult<Models.Agreement>>
        {
            string _accessToken;

            [Required]
            [FromRoute]
            public Guid? SaleId { get; set; }

            [Required]
            public string AccessToken
            {
                get => _accessToken;
                set => _accessToken = $"Bearer {value}";
            }

            [Required]
            public string Number { get; set; }

            public string Notes { get; set; }

            [Required]
            public PaymentInfo Payment { get; set; }

            public PhoneNumber Phone { get; set; }

            public string Email { get; set; }

            public string Contact { get; set; }

            [Required]
            public Address MailingAddress { get; set; }

            public ICollection<Beneficiary> Beneficiaries { get; set; } = new Collection<Beneficiary>();

            [Required]
            public Modality? Modality { get; set; }

            [Required]
            public DentalCare DentalCare { get; set; }
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
                    return CommandResult<Models.Agreement>.Fail("Não é possivel criar um contato para esta venda!");

                LegalPersonAgreement personalData = new LegalPersonAgreement
                {
                    Phone = command.Phone,
                    Email = command.Email,
                    Contact = command.Contact,
                    MailingAddress = command.MailingAddress,
                    Modality = command.Modality.Value,
                    DentalCare = command.DentalCare,
                    Beneficiaries = command.Beneficiaries
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