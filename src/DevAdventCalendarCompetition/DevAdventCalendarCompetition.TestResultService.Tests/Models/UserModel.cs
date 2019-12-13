using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.TestResultService.Tests.Models
{
    internal static class UserModel
    {
        internal readonly static ApplicationUser userA = new ApplicationUser()
        {
            Id = "1",
            UserName = "A",
            Email = "a@test.com"
        };

        internal readonly static ApplicationUser userB = new ApplicationUser()
        {
            Id = "2",
            UserName = "B",
            Email = "b@test.com"
        };

        internal readonly static ApplicationUser userC = new ApplicationUser()
        {
            Id = "3",
            UserName = "C",
            Email = "c@test.com"
        };

        internal readonly static ApplicationUser userD = new ApplicationUser()
        {
            Id = "4",
            UserName = "D",
            Email = "d@test.com"
        };

        internal readonly static ApplicationUser userE = new ApplicationUser()
        {
            Id = "5",
            UserName = "E",
            Email = "e@test.com"
        };

        internal readonly static ApplicationUser userF = new ApplicationUser()
        {
            Id = "6",
            UserName = "F",
            Email = "f@test.com"
        };

        internal readonly static ApplicationUser userG = new ApplicationUser()
        {
            Id = "7",
            UserName = "G",
            Email = "g@test.com"
        };

        internal readonly static ApplicationUser userH = new ApplicationUser()
        {
            Id = "8",
            UserName = "H",
            Email = "h@test.com"
        };

        internal readonly static ApplicationUser userI = new ApplicationUser()
        {
            Id = "9",
            UserName = "I",
            Email = "i@test.com"
        };

        internal static void PrepareUserRows(ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            
            dbContext.Users.Add(userA);            
            dbContext.Users.Add(userB);           
            dbContext.Users.Add(userC);            
            dbContext.Users.Add(userD);
            dbContext.Users.Add(userE);
            dbContext.Users.Add(userF);
            dbContext.Users.Add(userG);
            dbContext.Users.Add(userH);
            dbContext.Users.Add(userI);
        }
    }
}
