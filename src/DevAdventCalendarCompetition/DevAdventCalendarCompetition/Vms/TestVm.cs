using System;
using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models
{
    public class TestVm
    {
        [Required(ErrorMessage = "Pole Numer jest obowiązkowe")]
        [Display(Name = "Numer")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Pole Opis jest obowiązkowe")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole Dzień rozpoczęcia jest obowiązkowe")]
        [Display(Name = "Dzień rozpoczęcia")]
        [DataType(DataType.Date, ErrorMessage = "Podaj poprawny format daty")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Pole Dzień zakończenia jest obowiązkowe")]
        [Display(Name = "Dzień zakończenia")]
        [DataType(DataType.Date, ErrorMessage = "Podaj poprawny format daty")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        [Required(ErrorMessage = "Pole Odpowiedź jest obowiązkowe")]
        [Display(Name = "Odpowiedź")]
        public string Answer { get; set; }

        [Display(Name = "Nazwa sponsora")]
        public string SponsorName { get; set; }

        [Display(Name = "Logo sponsora (ścieżka)")]
        public string SponsorLogoUrl { get; set; }
    }
}