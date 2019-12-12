using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.TestResultService.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.TestAnswerResultService.TestAnswers.Models
{
    internal class TestWrongAnswerModel
    {
        private readonly UserModel _userModel;
        private readonly TestModel _testModel;

        internal TestWrongAnswerModel(UserModel userModel, TestModel testModel)
        {
            this._userModel = userModel;
            this._testModel = testModel;
        }

        public void PrepareTestWrongAnswerRows(ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            //userD - no wrong answers

            //userC - no wrong answers

            //userB - 2 wrong answers

            dbContext.TestWrongAnswer.Add(new TestWrongAnswer()
            {
                Id = 1,
                UserId = this._userModel.userB.Id,
                User = this._userModel.userB,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(10)
            });

            dbContext.TestWrongAnswer.Add(new TestWrongAnswer()
            {
                Id = 2,
                UserId = this._userModel.userB.Id,
                User = this._userModel.userB,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(11)
            });

            //userA - 2 wrong answers

            dbContext.TestWrongAnswer.Add(new TestWrongAnswer()
            {
                Id = 3,
                UserId = this._userModel.userA.Id,
                User = this._userModel.userA,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(10)
            });

            dbContext.TestWrongAnswer.Add(new TestWrongAnswer()
            {
                Id = 8,
                UserId = this._userModel.userA.Id,
                User = this._userModel.userA,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                Time = this._testModel.test2.StartDate.Value.AddHours(25)
            });
        }
    }
}
