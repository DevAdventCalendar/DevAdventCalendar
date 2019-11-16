using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Vms
{
    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Pole Email jest obowiązkowe")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}