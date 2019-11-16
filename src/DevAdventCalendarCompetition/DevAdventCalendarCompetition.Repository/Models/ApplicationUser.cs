using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DevAdventCalendarCompetition.Repository.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public ICollection<TestAnswer> Answers { get; private set; }
    }
}