using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}