using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DevAdventCalendarCompetition.Repository.Models
{
    [Table("Results")]
    public class Result : ModelBase
    {
        [Required]
        [MaxLength(450)]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public int CorrectAnswersCount { get; set; }

        [Required]
        public int WrongAnswersCount { get; set; }

        [Required]
        public int Points { get; set; }

        public ApplicationUser User { get; set; }
    }
}
