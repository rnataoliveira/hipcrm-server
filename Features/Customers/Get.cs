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
    public class Query : IRequest<CustomerViewModel>
    {
      [Required]
      public Guid? CustomerId { get; set; }
    }

    public class Handler : AsyncRequestHandler<Query, CustomerViewModel>
    {
      readonly ApplicationDbContext _context;

      public Handler(ApplicationDbContext context)
      {
        _context = context;
      }

      protected override async Task<CustomerViewModel> Handle(Query request)
      {
        var customer = await _context.Customers.Include(c => c.PersonalData)
          .FirstOrDefaultAsync(c => c.Id == request.CustomerId);

          return customer != null ? new CustomerViewModel(customer) : null;
      }
    }

    public class CustomerViewModel : Customer
    {
      public CustomerViewModel(Customer customer)
      {
        base.Id = customer.Id;
        base.Notes = customer.Notes;
        base.PersonalData = customer.PersonalData;
      }

      public string Type => this.PersonalData.GetType().Name.ToString();
    }
  }
}