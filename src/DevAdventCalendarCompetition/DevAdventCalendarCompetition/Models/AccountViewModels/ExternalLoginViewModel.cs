using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowi¹zkowe")]
        [EmailAddress(ErrorMessage = "Podaj prawid³owy format adresu email")]
        public string Email { get; set; }
    }
}