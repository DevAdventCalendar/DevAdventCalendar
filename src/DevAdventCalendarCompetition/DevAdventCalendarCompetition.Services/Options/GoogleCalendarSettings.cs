using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Services.Options
{
    public class GoogleCalendarSettings
    {
        [Required]
        public string Summary { get; set; }

        [Required]
        public CalendarEvent Events { get; set; }

        [Required]
#pragma warning disable CA1056 // Uri properties should not be strings
        public string CalendarsUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings

        [Required]
#pragma warning disable CA1056 // Uri properties should not be strings
        public string EventsUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings
    }
}
