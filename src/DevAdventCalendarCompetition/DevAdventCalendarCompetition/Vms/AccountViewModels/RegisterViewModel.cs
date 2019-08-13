using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Vms
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [EmailAddress(ErrorMessage = "Podaj poprawny format email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Imię jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Pole Imię może mieć maksymalnie 100 znaków")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole Nazwisko jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Pole Nazwisko może mieć maksymalnie 100 znaków")]
        [Display(Name = "Nazwisko")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Pole Telefon jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Pole Telefon może mieć maksymalnie 100 znaków")]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Długość {0} powinna być większa niż {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole Hasło potwierdzające jest obowiązkowe")]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i hasło potwierdzające nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }
    }
}