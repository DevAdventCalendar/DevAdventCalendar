using System;
using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models
{
    public class TestViewModel
    {
        [Required]
        [Display(Name = "Numer")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Dzień rozpoczęcia")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Dzień zakończenia")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        [Required]
        [Display(Name = "Odpowiedź")]
        public string Answer { get; set; }

        [Display(Name = "Nazwa sponsora")]
        public string SponsorName { get; set; }

        [Display(Name = "Logo sponsora (ścieżka)")]
        public string SponsorLogoUrl { get; set; }
    }
}