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
    public class DeleteSale
    {
        public class Command : IRequest<CommandResult>
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

            protected async override Task<CommandResult> Handle(Command deleteSale)
            {
                SalePipeline sale = await _dbContext.SalesPipelines.FindAsync(deleteSale.SaleId.Value);
                if(sale == null)
                    return CommandResult.Fail($"Sale not found with Id: {deleteSale.SaleId}");

                _dbContext.Remove(sale);
                
                await _mediator.Publish(new Deleted() { Sale = sale, AccessToken = deleteSale.AccessToken });

                await _dbContext.SaveChangesAsync();
                return CommandResult.Success;
            }
        }

        public class Deleted : INotification
        {
            public SalePipeline Sale { get; set; }

            public string AccessToken { get; set; }
        }
    }
}