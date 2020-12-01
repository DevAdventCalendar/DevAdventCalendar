using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DevAdventCalendarCompetition.Repository.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool EmailNotificationsEnabled { get; set; }

        public bool PushNotificationsEnabled { get; set; }

        public bool IsIntegratedWithGoogleCalendar { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<UserTestCorrectAnswer> CorrectAnswers { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<UserTestWrongAnswer> WrongAnswers { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}