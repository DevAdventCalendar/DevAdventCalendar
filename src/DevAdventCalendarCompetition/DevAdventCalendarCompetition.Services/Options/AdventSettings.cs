using System;

namespace DevAdventCalendarCompetition.Services.Options
{
    public class AdventSettings
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsAdvent()
        {
            var now = DateTime.Now;
            return this.StartDate <= now && this.EndDate >= now;
        }
    }
}
