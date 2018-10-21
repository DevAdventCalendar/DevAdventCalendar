using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Repository.Models
{
    public class ModelBase
    {
        [Key]
        public int Id { get; set; }
    }
}