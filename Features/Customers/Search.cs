using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using server.Data;
using server.Models;
using server.Shared;

namespace server.Features.Customers
{
    public class Search
    {
        public class Query : IRequest<IEnumerable<CustomerSearchResult>>
        {
            public string Q { get; set; }
        }

        public class Handler : AsyncRequestHandler<Query, IEnumerable<CustomerSearchResult>>
        {
            readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            protected override async Task<IEnumerable<CustomerSearchResult>> Handle(Query request)
            {
                return _context.Customers
                    .Select(customer => new CustomerSearchResult()
                    {
                        CustomerId = customer.Id
                    });
            }
        }

        public class CustomerSearchResult
        {
            public Guid CustomerId { get; set; }

            public string Name { get; set; }

            public string OfficialDocument { get; set; }
        }
    }
}