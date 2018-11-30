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
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Pole Dzień zakończenia jest obowiązkowe")]
        [Display(Name = "Dzień zakończenia")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Pole Odpowiedź jest obowiązkowe")]
        [Display(Name = "Odpowiedź")]
        public string Answer { get; set; }

        [Display(Name = "Nazwa sponsora")]
        public string SponsorName { get; set; }

        [Display(Name = "Logo sponsora (ścieżka)")]
        public string SponsorLogoUrl { get; set; }

        [Required(ErrorMessage = "Pole Zniżka jest obowiązkowe")]
        [Display(Name = "Zniżka")]
        public string Discount { get; set; }

        [Required(ErrorMessage = "Pole Adres zniżki jest obowiązkowe")]
        [Display(Name = "Adres zniżki")]
        public string DiscountUrl { get; set; }

        [Required(ErrorMessage = "Pole Adres loga zniżki jest obowiązkowe")]
        [Display(Name = "Adres loga zniżki")]
        public string DiscountLogoUrl { get; set; }
    }
}