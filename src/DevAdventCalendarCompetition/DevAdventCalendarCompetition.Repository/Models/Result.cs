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

        public int Week1Points { get; set; }

        public int Week1Place { get; set; }

        public int Week2Points { get; set; }

        public int Week2Place { get; set; }

        public int Week3Points { get; set; }

        public int Week3Place { get; set; }

        public int FinalPoints { get; set; }

        public int FinalPlace { get; set; }

        public ApplicationUser User { get; set; }
    }
}
