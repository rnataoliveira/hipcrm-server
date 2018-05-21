using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Shared;
using Dapper;
using StackExchange.Profiling.Data;
using StackExchange.Profiling;

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
      readonly IDbConnection _connection;

      public Handler(ApplicationDbContext context)
      {
        _connection = new ProfiledDbConnection(context.Database.GetDbConnection(), MiniProfiler.Current);
      }

      readonly string sqlQuery =
          @"SELECT
                C.Id,
                (
                    CASE WHEN P.Discriminator = 'PhysicalPerson'
                    THEN CONCAT(P.FirstName, ' ', P.Surname)
                    ELSE P.CompanyName
                    END
                ) Name,
                (
                    CASE WHEN P.Discriminator = 'PhysicalPerson'
                    THEN P.DocumentNumber
                    ELSE P.CompanyRegistration
                    END
                ) DocumentNumber
            FROM Customer C
            INNER JOIN PersonalData P ON P.Id = C.PersonalDataId
            WHERE 
                (
                    CONCAT(P.FirstName, ' ', P.Surname) LIKE '%' + @Q + '%' OR
                    P.CompanyName LIKE '%' + @Q + '%'
                )
            OR
                (
                    P.DocumentNumber = @Q OR
                    P.CompanyRegistration = @Q
                )
            ";

      protected override async Task<IEnumerable<CustomerSearchResult>> Handle(Query request)
        => await _connection.QueryAsync<CustomerSearchResult>(sqlQuery, new { Q = request.Q });

    }

    public class CustomerSearchResult
    {
      public Guid Id { get; set; }

      public string Name { get; set; }

      public string DocumentNumber { get; set; }
    }
  }
}