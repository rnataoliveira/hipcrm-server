using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using server.Shared;

namespace server.Features.Customers.Create
{
    public class CreateLegalPersonCustomer
    {
        public class Command : IRequest<CommandResult<Guid>>
        {
            string _accessToken;

            [Required]
            public string AccessToken
            {
                get => _accessToken;
                set => _accessToken = $"Bearer {value}";
            }

            [Required]
            public string CompanyRegistration { get; set; }

            [Required]
            public string CompanyName { get; set; }

            public string StateRegistration { get; set; }

            public PhoneNumber Phone { get; set; }

            public Address Address { get; set; }

            [EmailAddress]
            public string Email { get; set; }
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

            protected async override Task<CommandResult<Guid>> Handle(Command command)
            {
                // Unicidade do cnpj
                if (_dbContext.LegalPersonsData.Any(data => data.CompanyRegistration == command.CompanyRegistration))
                    return CommandResult<Guid>.Fail($"A Customer with this Company Registration number already exists.");

                //Intanciar um novo customer
                var customer = new Customer();

                // Instanciar LegalPerson Data
                var personalData = new LegalPerson
                {
                    CompanyRegistration = command.CompanyRegistration,
                    CompanyName = command.CompanyName,
                    StateRegistration = command.StateRegistration,
                    Phone = command.Phone,
                    Email = command.Email,
                    Address = command.Address
                };

                // Associar os dados pessoais ao novo customer
                customer.PersonalData = personalData;

                // Adicionar o customer no database
                await _dbContext.Customers.AddAsync(customer);
                
                // Notificar sobre a criação do customer
                await _mediator.Publish(new Created { CustomerId = customer.Id, AccessToken = command.AccessToken });

                // Comitar a transação no banco de dados
                await _dbContext.SaveChangesAsync();

                // Retornar o resultado do comando com o Id do Novo Customer
                return CommandResult<Guid>.Success(customer.Id);
            }
        }

        public class Created : INotification
        {
            public Guid CustomerId { get; set; }

            public string AccessToken { get; set; }
        }
    }
}