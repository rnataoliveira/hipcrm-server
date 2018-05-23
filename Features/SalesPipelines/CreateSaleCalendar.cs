using System;
using System.Threading.Tasks;
using MediatR;
using Google.Apis.Services;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using server.Facades.Google;
using server.Facades.Google.Models;
using server.Data;
using server.Models;
using Refit;
using Newtonsoft.Json.Linq;

namespace server.Features.SalesPipelines
{
    public class CreateSaleCalendar
    {
        public class Command : IRequest
        {
            public Guid SaleId { get; set; }

            public string AccessToken { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            readonly ICalendarApi _calendarApi;
            readonly ApplicationDbContext _context;

            public Handler(ICalendarApi calendarApi, ApplicationDbContext context)
            {
                _calendarApi = calendarApi;
                _context = context;
            }

            protected override async Task Handle(Command request)
            {
                SalePipeline sale = await _context.SalesPipelines.FindAsync(request.SaleId);

                var calendar = new Calendar
                {
                    Summary = $"Sale: {sale.Id}",
                    Description = $"Sale to: {sale.Customer.Id}",
                    TimeZone = "America/Sao_Paulo"
                };

                Calendar newCalendar = await _calendarApi.CreateCalendar(calendar, request.AccessToken);

                sale.CalendarId = newCalendar.Id;

                _context.Update(sale);
                await _context.SaveChangesAsync();
            }
        }
    }
}