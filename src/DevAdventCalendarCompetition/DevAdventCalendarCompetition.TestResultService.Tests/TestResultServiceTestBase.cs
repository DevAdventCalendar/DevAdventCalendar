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
        private readonly UserModel userModel;
        private readonly TestModel testModel;
        private readonly TestAnswerModel testAnswerModel;
        private readonly TestWrongAnswerModel testWrongAnswerModel;
        private ApplicationDbContext dbContext;

        public TestResultServiceTestBase()
        {
            userModel = new UserModel();
            testModel = new TestModel();
            testAnswerModel = new TestAnswerModel(userModel, testModel);
            testWrongAnswerModel = new TestWrongAnswerModel(userModel, testModel);
            
        }

        public List<Result> GetExpectedResultModel()
        {
            var resultModel = new ResultModel(userModel);
            return resultModel.GetResultList(dbContext);
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
                userModel.PrepareUserRows(dbContext);
                await dbContext.SaveChangesAsync();
            }

            if (await dbContext.Test.CountAsync() <= 0)
            {
                testModel.PrepareTestRows(dbContext);
                await dbContext.SaveChangesAsync();
            }

            if (await dbContext.TestAnswer.CountAsync() <= 0)
            {
                testAnswerModel.PrepareTestAnswerRows(dbContext);
                await dbContext.SaveChangesAsync();
            }

            if (await dbContext.TestWrongAnswer.CountAsync() <= 0)
            {
                testWrongAnswerModel.PrepareTestWrongAnswerRows(dbContext);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
