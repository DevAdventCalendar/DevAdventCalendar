using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.Manage
{
    public class IndexViewModel
    {
        [Display(Name = "Nazwa użytkownika")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy format adresu email")]
        public string Email { get; set; }

        [Display(Name = "Chcę otrzymywać notyfikacje email")]
        public bool EmailNotificationsEnabled { get; set; }

        [Display(Name = "Chcę otrzymywać notyfikacje push")]
        public bool PushNotificationsEnabled { get; set; }

        public string StatusMessage { get; set; }
    }
}