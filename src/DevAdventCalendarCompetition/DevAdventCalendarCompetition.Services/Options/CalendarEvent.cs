using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Services.Options
{
    public class CalendarEvent
    {
        [Required]
        public string Summary { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string TimeZone { get; set; }

        [Required]
        public string ReminderMethod { get; set; }

        [Required]
        public int ReminderMinutes { get; set; }
    }
}