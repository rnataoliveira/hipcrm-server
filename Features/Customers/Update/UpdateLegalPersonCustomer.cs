using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using server.Shared;
using Microsoft.EntityFrameworkCore;

namespace server.Features.Customers.Update
{
    public class UpdateLegalPersonCustomer
    {
        public class Command : IRequest<CommandResult<Customer>>
        {
            public Guid CustomerId { get; set; }

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

            [EmailAddress]
            public string Email { get; set; }
            
            public Address Address { get; set; }

            public string Notes { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, CommandResult<Customer>>
        {
            readonly IMediator _mediator;
            readonly ApplicationDbContext _dbContext;

            public Handler(IMediator mediator, ApplicationDbContext dbContext)
            {
                _mediator = mediator;
                _dbContext = dbContext;
            }
            protected async override Task<CommandResult<Customer>> Handle(Command command)
            {
                //buscar o cliente
                var customer = await _dbContext.Customers
                    .Include(c => c.PersonalData)
                    .FirstOrDefaultAsync(c => c.Id == command.CustomerId);

                //verificar se o cliente existe
                if (customer == null)
                    return CommandResult<Customer>.Fail($"Customer not found with id: {command.CustomerId}");

                //atualizar as propriedades devidas
                customer.Notes = command.Notes;

                var personalData = customer.PersonalData as LegalPerson;
                personalData.CompanyName = command.CompanyName;
                personalData.CompanyRegistration = command.CompanyRegistration;
                personalData.StateRegistration = command.StateRegistration;
                personalData.Phone = command.Phone;
                personalData.Email = command.Email;
                personalData.Address = command.Address;

                customer.PersonalData = personalData;

                //marcar como atualizado no context
                _dbContext.Update(customer);

                //publicar o evento
                await _mediator.Publish(new Updated
                {
                    CustomerId = command.CustomerId,
                    AccessToken = command.AccessToken
                });

                //salvar o update
                await _dbContext.SaveChangesAsync();

                //retornar o resultado
                return CommandResult<Customer>.Success(customer);
            }
        }
    }
}