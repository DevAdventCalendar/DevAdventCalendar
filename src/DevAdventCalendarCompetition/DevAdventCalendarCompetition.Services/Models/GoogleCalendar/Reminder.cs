using System.Collections.Generic;

namespace DevAdventCalendarCompetition.Services.Models.GoogleCalendar
{
    public class Reminder
    {
        public bool UseDefault { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public List<Override> Overrides { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
