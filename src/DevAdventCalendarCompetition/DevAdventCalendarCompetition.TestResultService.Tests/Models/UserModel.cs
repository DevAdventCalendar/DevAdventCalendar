using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.TestResultService.Tests.Models
{
    internal class UserModel
    {
        internal readonly ApplicationUser userA = new ApplicationUser()
        {
            Id = "1",
            UserName = "A",
            Email = "a@test.com"
        };

        internal readonly ApplicationUser userB = new ApplicationUser()
        {
            Id = "2",
            UserName = "B",
            Email = "b@test.com"
        };

        internal readonly ApplicationUser userC = new ApplicationUser()
        {
            Id = "3",
            UserName = "C",
            Email = "c@test.com"
        };

        internal readonly ApplicationUser userD = new ApplicationUser()
        {
            Id = "4",
            UserName = "D",
            Email = "d@test.com"
        };

        internal readonly ApplicationUser userE = new ApplicationUser()
        {
            Id = "5",
            UserName = "E",
            Email = "e@test.com"
        };

        internal readonly ApplicationUser userF = new ApplicationUser()
        {
            Id = "6",
            UserName = "F",
            Email = "f@test.com"
        };

        internal readonly ApplicationUser userG = new ApplicationUser()
        {
            Id = "7",
            UserName = "G",
            Email = "g@test.com"
        };

        internal readonly ApplicationUser userH = new ApplicationUser()
        {
            Id = "8",
            UserName = "H",
            Email = "h@test.com"
        };

        internal readonly ApplicationUser userI = new ApplicationUser()
        {
            Id = "9",
            UserName = "I",
            Email = "i@test.com"
        };

        internal void PrepareUserRows(ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            
            dbContext.Users.Add(this.userA);            
            dbContext.Users.Add(this.userB);           
            dbContext.Users.Add(this.userC);            
            dbContext.Users.Add(this.userD);
            dbContext.Users.Add(this.userE);
            dbContext.Users.Add(this.userF);
            dbContext.Users.Add(this.userG);
            dbContext.Users.Add(this.userH);
            dbContext.Users.Add(this.userI);
        }
    }
}
