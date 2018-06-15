using System.Threading.Tasks;
using MediatR;
using server.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using server.Data;
using server.Models;
using System.Collections.Generic;
using server.Facades.Google.Models;
using System.Linq;
using server.Facades.Google;

namespace server.Features.SalesPipelines
{
    public class GetAllAppointments
    {
        public class Query : IRequest<IEnumerable<Event>>
        {
            string _accessToken;

            [Required]
            public string AccessToken
            {
                get => _accessToken;
                set => _accessToken = $"Bearer {value}";
            }
        }

        public class Handler : AsyncRequestHandler<Query, IEnumerable<Event>>
        {
            readonly ApplicationDbContext _context;
            readonly ICalendarApi _calendarApi;

            public Handler(ApplicationDbContext context, ICalendarApi calendarApi)
            {
                _context = context;
                _calendarApi = calendarApi;
            }

            protected async override Task<IEnumerable<Event>> Handle(Query request)
            {
                IEnumerable<string> calendarIds = _context.SalesPipelines
                    .Where(s => s.Stage == SaleStage.Proposal)
                    .Select(s => s.CalendarId);

                var events = new List<Event>();
                foreach (var calendarId in calendarIds)
                {
                    CalendarEvents calendarEvents = await _calendarApi.GetEvents(calendarId, request.AccessToken);
                    events.AddRange(calendarEvents.Items);
                }

                return events;
            }
        }
    }
}