using Refit;
using System;
using System.Threading.Tasks;
using server.Facades.Google.Models;
using System.Net.Http;

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
    }
}