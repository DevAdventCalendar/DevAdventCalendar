using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.TestResultService.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.TestAnswerResultService.TestAnswers.Models
{
    internal class TestAnswerModel
    {
        private UserModel _userModel;
        private TestModel _testModel;

        internal TestAnswerModel(UserModel userModel, TestModel testModel)
        {
            this._userModel = userModel;
            this._testModel = testModel;
        }

        public void PrepareTestAnswerRows(ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            //userD

            dbContext.TestAnswer.Add(new TestAnswer()
            {
                Id = 1,
                UserId = this._userModel.userD.Id,
                User = this._userModel.userD,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddMinutes(10),
                AnsweringTimeOffset = new TimeSpan(0, 10, 0)
            });

            dbContext.TestAnswer.Add(new TestAnswer()
            {
                Id = 2,
                UserId = this._userModel.userD.Id,
                User = this._userModel.userD,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                AnsweringTime = this._testModel.test2.StartDate.Value.AddMinutes(5),
                AnsweringTimeOffset = new TimeSpan(0, 5, 0)
            });

            //userC

            dbContext.TestAnswer.Add(new TestAnswer()
            {
                Id = 3,
                UserId = this._userModel.userC.Id,
                User = this._userModel.userC,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddMinutes(10),
                AnsweringTimeOffset = new TimeSpan(0, 10, 0)
            });

            dbContext.TestAnswer.Add(new TestAnswer()
            {
                Id = 4,
                UserId = this._userModel.userC.Id,
                User = this._userModel.userC,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                AnsweringTime = this._testModel.test2.StartDate.Value.AddMinutes(15),
                AnsweringTimeOffset = new TimeSpan(0, 15, 0)
            });

            //userB

            dbContext.TestAnswer.Add(new TestAnswer()
            {
                Id = 5,
                UserId = this._userModel.userB.Id,
                User = this._userModel.userB,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddHours(2),
                AnsweringTimeOffset = new TimeSpan(2, 0, 0)
            });

            dbContext.TestAnswer.Add(new TestAnswer()
            {
                Id = 6,
                UserId = this._userModel.userB.Id,
                User = this._userModel.userB,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                AnsweringTime = this._testModel.test2.StartDate.Value.AddHours(2).AddMilliseconds(10),
                AnsweringTimeOffset = new TimeSpan(2, 0, 0, 0, 10)
            });

            //userA

            dbContext.TestAnswer.Add(new TestAnswer()
            {
                Id = 7,
                UserId = this._userModel.userA.Id,
                User = this._userModel.userA,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddHours(2).AddMilliseconds(5),
                AnsweringTimeOffset = new TimeSpan(2, 0, 0, 0, 5)
            });

            dbContext.TestAnswer.Add(new TestAnswer()
            {
                Id = 8,
                UserId = this._userModel.userA.Id,
                User = this._userModel.userA,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                AnsweringTime = this._testModel.test2.StartDate.Value.AddHours(2),
                AnsweringTimeOffset = new TimeSpan(2, 0, 0)
            }); ;
        }
    }
}
