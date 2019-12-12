using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.TestResultService.Tests.Models
{
    internal class TestModel
    {
        internal readonly Test test1 = new Test()
        {
            Id = 1,
            StartDate = new DateTime(DateTime.Today.Year, 12, 1, 20, 0, 0),
            EndDate = new DateTime(DateTime.Today.Year, 12, 24, 23, 59, 0)
        };

        internal readonly Test test2 = new Test()
        {
            Id = 2,
            StartDate = new DateTime(DateTime.Today.Year, 12, 2, 20, 0, 0),
            EndDate = new DateTime(DateTime.Today.Year, 12, 24, 23, 59, 0)
        };

        internal readonly Test test3 = new Test()
        {
            Id = 3,
            StartDate = new DateTime(DateTime.Today.Year, 12, 3, 20, 0, 0),
            EndDate = new DateTime(DateTime.Today.Year, 12, 24, 23, 59, 0)
        };

        internal readonly Test test4 = new Test()
        {
            Id = 4,
            StartDate = new DateTime(DateTime.Today.Year, 12, 4, 20, 0, 0),
            EndDate = new DateTime(DateTime.Today.Year, 12, 24, 23, 59, 0)
        };

        internal readonly Test test5 = new Test()
        {
            Id = 5,
            StartDate = new DateTime(DateTime.Today.Year, 12, 5, 20, 0, 0),
            EndDate = new DateTime(DateTime.Today.Year, 12, 24, 23, 59, 0)
        };

        internal readonly Test test6 = new Test()
        {
            Id = 6,
            StartDate = new DateTime(DateTime.Today.Year, 12, 6, 20, 0, 0),
            EndDate = new DateTime(DateTime.Today.Year, 12, 24, 23, 59, 0)
        };

        internal readonly Test test7 = new Test()
        {
            Id = 7,
            StartDate = new DateTime(DateTime.Today.Year, 12, 7, 20, 0, 0),
            EndDate = new DateTime(DateTime.Today.Year, 12, 24, 23, 59, 0)
        };

        internal readonly Test test8 = new Test()
        {
            Id = 8,
            StartDate = new DateTime(DateTime.Today.Year, 12, 8, 20, 0, 0),
            EndDate = new DateTime(DateTime.Today.Year, 12, 24, 23, 59, 0)
        };
        
        internal void PrepareTestRows(ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            dbContext.Test.Add(this.test1);
            dbContext.Test.Add(this.test2);
            dbContext.Test.Add(this.test3);
            dbContext.Test.Add(this.test4);
            dbContext.Test.Add(this.test5);
            dbContext.Test.Add(this.test6);
            dbContext.Test.Add(this.test7);
            dbContext.Test.Add(this.test8);
        }
    }
}
