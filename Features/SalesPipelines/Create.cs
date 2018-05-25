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

namespace server.Features.SalesPipelines
{
    public class Create
    {
        public class Command : IRequest<CommandResult<Guid>>
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

        public class Handler : AsyncRequestHandler<Command, CommandResult<Guid>>
        {
            readonly IMediator _mediator;
            readonly ApplicationDbContext _dbContext;

            public Handler(IMediator mediator, ApplicationDbContext dbContext)
            {
                _mediator = mediator;
                _dbContext = dbContext;
            }

            protected async override Task<CommandResult<Guid>> Handle(Command createSale)
            {
                // Busca o Cliente pelo Id
                Customer customer = await _dbContext.Customers.FindAsync(createSale.CustomerId);
                // Verifica se o Cliente existe
                if (customer == null)
                    return CommandResult<Guid>.Fail($"Customer not found with Id: {createSale.CustomerId}");

                // Verifica se ja existe alguma venda para o Cliente BUscado
                bool alreadyExists = _dbContext.SalesPipelines.Any(sale => sale.Customer.Id == customer.Id);
                if (alreadyExists)
                    return CommandResult<Guid>.Fail($"A Sale already exists for this customer");

                var salePipeline = new SalePipeline(customer);

                var hashIds = new Hashids(salePipeline.Id.ToString(), 4);
                var hashCode = salePipeline.Id.GetHashCode();
                salePipeline.Code = hashIds.Encode(hashCode < 0 ? (hashCode * (-1)) : hashCode);

                await _dbContext.AddAsync(salePipeline);
                await _dbContext.SaveChangesAsync();

                await _mediator.Publish(new Created() { SaleId = salePipeline.Id, AccessToken = createSale.AccessToken });
                return CommandResult<Guid>.Success(salePipeline.Id);
            }
        }

        public class Created : INotification
        {
            public Guid SaleId { get; set; }

            public string AccessToken { get; set; }
        }
    }
}