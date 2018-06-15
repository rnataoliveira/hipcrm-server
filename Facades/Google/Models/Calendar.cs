using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace server.Facades.Google.Models
{
    public class Calendar : Resource
    {
        public string Summary { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string TimeZone { get; set; }
    }

    public class CalendarEvents : Resource 
    {
        public IEnumerable<Event> Items { get; set; }
    }

    public class Event : Resource
    {
        public string Summary { get; set; }

        public string Description { get; set; }

        public EventTime Start { get; set; }

        public EventTime End { get; set; }

        public bool EndTimeUnspecified { get; set; }

        public string HtmlLink { get; set; }
    }

    public class EventTime
    {
        public DateTime Date { get; set; }

        public DateTime DateTime { get; set; }

        public string TimeZone { get; set; }
    }
}