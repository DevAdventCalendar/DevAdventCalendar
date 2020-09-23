using System.Collections.Generic;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Models.GoogleCalendar
{
    public class IndexViewModel
    {
        public IEnumerable<Items> Calendars { get; set; }

        public bool HasPermissions { get; set; }

        public string StatusMessage { get; set; }
    }
}
