using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowi�zkowe")]
        [EmailAddress(ErrorMessage = "Podaj prawid�owy format adresu email")]
        public string Email { get; set; }
    }
}