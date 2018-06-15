using Refit;
using System;
using System.Threading.Tasks;
using server.Facades.Google.Models;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;

namespace server.Facades.Google
{
    public interface ICalendarApi 
    {
        [Get("/calendars/{calendarId}")]
        Task<Calendar> GetCalendar(string calendarId, [Header("Authorization")] string authorization);

        [Delete("/calendars/{calendarId}")]
        Task DeleteCalendar(string calendarId, [Header("Authorization")] string authorization);

        [Post("/calendars")]
        Task<Calendar> CreateCalendar([Body(true)] Calendar calendar, [Header("Authorization")] string authorization);

        [Get("/calendars/{calendarId}/events")]
        Task<CalendarEvents> GetEvents(string calendarId, [Header("Authorization")] string authorization);
    }
}