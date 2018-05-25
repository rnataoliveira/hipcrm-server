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
    public class DeleteSaleCalendar
    {
        public class Command : IRequest
        {
            public string CalendarId { get; set; }

            public string AccessToken { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            readonly ICalendarApi _calendarApi;

            public Handler(ICalendarApi calendarApi, ApplicationDbContext context)
            {
                _calendarApi = calendarApi;
            }

            protected override async Task Handle(Command request) 
                => await _calendarApi.DeleteCalendar(request.CalendarId, request.AccessToken);
        }
    }
}