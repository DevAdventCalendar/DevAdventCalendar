using System;
using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Services.Options
{
    public class TestHours
    {
        [Required]
        public TimeSpan StartHour { get; set; }

        [Required]
        public TimeSpan EndHour { get; set; }
    }
}
