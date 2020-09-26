using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Models.GoogleCalendar
{
    public class AddCalendarViewModel
    {
        [Required(ErrorMessage = "Pole Nazwa kalendarza jest obowiązkowe")]
        [Display(Name = "Nazwa kalendarza")]
        public string CalendarSummary { get; set; }

        [Required(ErrorMessage = "Pole Data początkowa jest obowiązkowe")]
        [Display(Name = "Od kiedy chcesz otrzymywać powiadomienia?")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Pole Data końcowa jest obowiązkowe")]
        [Display(Name = "Do kiedy chcesz otrzymywać powiadomienia?")]
        public DateTime EndDate { get; set; }
    }
}
