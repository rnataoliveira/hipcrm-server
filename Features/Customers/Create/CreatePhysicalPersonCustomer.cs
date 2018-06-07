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
    public class CreatePhysicalPersonCustomer
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
            public string FirstName { get; set; }

            [Required]
            public string Surname { get; set; }

            [Required]
            public string DocumentNumber { get; set; }

            public string GeneralRegistration { get; set; }

            [Required]
            public DateTime BirthDate { get; set; }

            [Required]
            public string Sex { get; set; }

            [Required]
            public string MaritalState { get; set; }

            public PhoneNumber Phone { get; set; }

            public PhoneNumber CellPhone { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            public Address Address { get; set; }

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

            protected async override Task<CommandResult<Guid>> Handle(Command command)
            {
                // Unicidade do cnpj
                if (_dbContext.PhysicalsPersonsData.Any(data => data.DocumentNumber == command.DocumentNumber))
                    return CommandResult<Guid>.Fail($"A Customer with this Document Number already exists.");

                //Intanciar um novo customer
                var customer = new Customer
                {
                    Notes = command.Notes
                };

                // Instanciar LegalPerson Data
                var personalData = new PhysicalPerson
                {
                    FirstName = command.FirstName,
                    Surname = command.Surname,
                    DocumentNumber = command.DocumentNumber,
                    GeneralRegistration = command.GeneralRegistration,
                    BirthDate = command.BirthDate,
                    Sex = command.Sex,
                    MaritalState = command.MaritalState,
                    Phone = command.Phone,
                    CellPhone = command.CellPhone,
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