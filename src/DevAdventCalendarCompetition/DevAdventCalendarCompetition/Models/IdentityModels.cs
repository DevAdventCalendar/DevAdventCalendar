//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;

//namespace DevAdventCalendarCompetition.Models
//{
//    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
//    public class ApplicationUser : IdentityUser
//    {
//        public string FirstName { get; set; }

//        public string SecondName { get; set; }
//        public ICollection<TestAnswer> Answers { get; set; }

//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//            // Add custom user claims here
//            return userIdentity;
//        }
//    }
//}