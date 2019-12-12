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
            // ToDo: User with: 1 correct answer and 1 wrong anwers (on different puzzle)
            // ToDo: User with: 2 correct answers and 2 wrong anwers (both on same puzzle)
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            //userE - 1 wrong answer (0 correct answers)

            dbContext.TestWrongAnswer.Add(new TestWrongAnswer()
            {
                Id = 11,
                UserId = this._userModel.userE.Id,
                User = this._userModel.userE,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(1)
            });

            //userD - no wrong answers (2 correct answers)

            //userC - no wrong answers (2 correct answers)

            //userB - 2 wrong answers (2 correct answers)

            dbContext.TestWrongAnswer.Add(new TestWrongAnswer()
            {
                Id = 1,
                UserId = this._userModel.userB.Id,
                User = this._userModel.userB,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(1)
            });

            dbContext.TestWrongAnswer.Add(new TestWrongAnswer()
            {
                Id = 2,
                UserId = this._userModel.userB.Id,
                User = this._userModel.userB,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(1)
            });

            //userA - 2 wrong answers (2 correct answers)

            dbContext.TestWrongAnswer.Add(new TestWrongAnswer()
            {
                Id = 3,
                UserId = this._userModel.userA.Id,
                User = this._userModel.userA,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(1)
            });

            dbContext.TestWrongAnswer.Add(new TestWrongAnswer()
            {
                Id = 8,
                UserId = this._userModel.userA.Id,
                User = this._userModel.userA,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                Time = this._testModel.test2.StartDate.Value.AddHours(1)
            });
        }
    }
}
