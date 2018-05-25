using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using server.Models;
using server.Data;
using server.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace server.Features.Customers
{
    public class GetCustomers
    {
        public class Query : IRequest<IEnumerable<Customer>>
        {
            public string Q { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, IEnumerable<Customer>>
        {
            readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            protected override async Task<IEnumerable<Customer>> Handle(Query request) => await _context.Customers
                .Include(customer => customer.PersonalData)
                .ToListAsync();
        }
    }
}