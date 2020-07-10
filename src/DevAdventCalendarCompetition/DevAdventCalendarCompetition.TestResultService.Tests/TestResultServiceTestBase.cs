using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.TestAnswerResultService.TestAnswers.Models;
using DevAdventCalendarCompetition.TestResultService.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.TestResultService.Tests
{
    public class TestResultServiceTestBase
    {
        private readonly TestModel testModel;
        private readonly TestAnswerModel testAnswerModel;
        private readonly TestWrongAnswerModel testWrongAnswerModel;
        private ApplicationDbContext dbContext;

        public TestResultServiceTestBase()
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

        public async Task<TestResultRepository> GetTestResultRepositoryAsync()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            dbContext = new ApplicationDbContext(optionsBuilder.Options);
            dbContext.Database.EnsureCreated();
            await PrepareDatabaseRows(dbContext);

            return new TestResultRepository(dbContext);
        }

        private async Task PrepareDatabaseRows(ApplicationDbContext dbContext)
        {
            if (await dbContext.Users.CountAsync() <= 0)
            {
                UserModel.PrepareUserRows(dbContext);
                await dbContext.SaveChangesAsync();
            }

            if (await dbContext.Tests.CountAsync() <= 0)
            {
                testModel.PrepareTestRows(dbContext);
                await dbContext.SaveChangesAsync();
            }

            if (await dbContext.UserTestCorrectAnswers.CountAsync() <= 0)
            {
                testAnswerModel.PrepareTestAnswerRows(dbContext);
                await dbContext.SaveChangesAsync();
            }

            if (await dbContext.UserTestWrongAnswers.CountAsync() <= 0)
            {
                testWrongAnswerModel.PrepareTestWrongAnswerRows(dbContext);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
