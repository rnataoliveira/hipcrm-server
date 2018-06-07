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
using Microsoft.EntityFrameworkCore;

namespace server.Features.Customers
{
    public class DeleteCustomer
    {
        public class Command : IRequest<CommandResult>
        {
            string _accessToken;

            [Required]
            public Guid? CustomerId { get; set; }

            [Required]
            public string AccessToken
            {
                get => _accessToken;
                set => _accessToken = $"Bearer {value}";
            }
        }

        public class Handler : AsyncRequestHandler<Command, CommandResult>
        {
            readonly IMediator _mediator;
            readonly ApplicationDbContext _dbContext;

            public Handler(IMediator mediator, ApplicationDbContext dbContext)
            {
                _mediator = mediator;
                _dbContext = dbContext;
            }

            protected async override Task<CommandResult> Handle(Command deleteCustomer)
            {
                if(await _dbContext.SalesPipelines.AnyAsync(sale => sale.Customer.Id == deleteCustomer.CustomerId))
                    return CommandResult.Fail($"This Customer can't be removed!");

                Customer customer = await _dbContext.Customers
                    .Include(c => c.PersonalData)
                    .FirstOrDefaultAsync(c => c.Id == deleteCustomer.CustomerId);

                if(customer == null)
                    return CommandResult.Fail($"Customer not found with Id: {deleteCustomer.CustomerId}");

                _dbContext.Remove(customer);
                _dbContext.Remove(customer.PersonalData);
                
                await _mediator.Publish(new Deleted() { Customer = customer, AccessToken = deleteCustomer.AccessToken });

                await _dbContext.SaveChangesAsync();
                return CommandResult.Success;
            }
        }

        public class Deleted : INotification
        {
            public Customer Customer { get; set; }

            public string AccessToken { get; set; }
        }
    }
}