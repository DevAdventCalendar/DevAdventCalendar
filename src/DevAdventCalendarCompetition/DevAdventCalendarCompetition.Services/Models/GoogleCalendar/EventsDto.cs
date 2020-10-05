using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services.Models.GoogleCalendar
{
    public class EventsDto
    {
        public string Summary { get; set; }

        public string Location { get; set; }

        public EventDate Start { get; set; }

        public EventDate End { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public List<string> Recurrence { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}