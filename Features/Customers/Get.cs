using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using server.Models;
using server.Data;
using server.Shared;
using Microsoft.EntityFrameworkCore;
using System.Data;
using StackExchange.Profiling.Data;
using StackExchange.Profiling;
using Dapper;

namespace server.Features.Customers
{
    public class Get
    {
        public class Query : IRequest<Customer>
        {
            [Required]
            public Guid? CustomerId { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, Customer>
        {
            readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            protected override async Task<Customer> Handle(Query request)
                => await _context.Customers.Include(customer => customer.PersonalData)
                    .FirstOrDefaultAsync(customer => customer.Id == request.CustomerId);
        }
    }
}