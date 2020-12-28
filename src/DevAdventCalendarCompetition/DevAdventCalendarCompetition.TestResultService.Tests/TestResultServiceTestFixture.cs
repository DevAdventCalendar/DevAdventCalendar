using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.TestAnswerResultService.TestAnswers.Models;
using DevAdventCalendarCompetition.TestResultService.Tests.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DevAdventCalendarCompetition.TestResultService.Tests
{
    public class TestResultServiceTestFixture : IDisposable
    {
        private readonly TestModel testModel;
        private readonly TestAnswerModel testAnswerModel;
        private readonly TestWrongAnswerModel testWrongAnswerModel;
        private ApplicationDbContext dbContext;
        private TestResultRepository testResultRepository;

        public TestResultServiceTestFixture()
        {
            testModel = new TestModel();
            testAnswerModel = new TestAnswerModel(testModel);
            testWrongAnswerModel = new TestWrongAnswerModel(testModel);
            
        }

        public List<Result> GetExpectedWeek1ResultModel()
        {
            var resultModel = new ResultModel();
            return resultModel.GetWeek1ResultList(dbContext);
        }

        public List<Result> GetExpectedWeek2ResultModel()
        {
            var resultModel = new ResultModel();
            return resultModel.GetWeek2ResultList(dbContext);
        }

        public List<Result> GetExpectedFinalResultModel()
        {
            var resultModel = new ResultModel();
            return resultModel.GetFinalResultList(dbContext);
        }

        public TestResultRepository GetTestResultRepository()
        {
            if (testResultRepository != null)
            {
                return testResultRepository;
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var databaseName = Guid.NewGuid().ToString();
            optionsBuilder.UseInMemoryDatabase(databaseName);
            dbContext = new ApplicationDbContext(optionsBuilder.Options);
            dbContext.Database.EnsureCreated();
            PrepareDatabaseRows();

            testResultRepository = new TestResultRepository(dbContext);
            return testResultRepository;
        }

        private void PrepareDatabaseRows()
        {
            if (!dbContext.Users.Any())
            {
                UserModel.PrepareUserRows(dbContext);
                dbContext.SaveChanges();
            }

            if (!dbContext.Tests.Any())
            {
                testModel.PrepareTestRows(dbContext);
                dbContext.SaveChanges();
            }

            if (!dbContext.UserTestCorrectAnswers.Any())
            {
                testAnswerModel.PrepareTestAnswerRows(dbContext);
                dbContext.SaveChanges();
            }

            if (!dbContext.UserTestWrongAnswers.Any())
            {
                testWrongAnswerModel.PrepareTestWrongAnswerRows(dbContext);
                dbContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
    
    [CollectionDefinition(nameof(TestResultServiceTestCollection))]
    public class TestResultServiceTestCollection : ICollectionFixture<TestResultServiceTestFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
