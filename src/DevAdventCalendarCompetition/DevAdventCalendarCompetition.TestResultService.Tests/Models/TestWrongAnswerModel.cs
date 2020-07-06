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
        private readonly TestModel _testModel;

        internal TestWrongAnswerModel(TestModel testModel)
        {
            this._testModel = testModel;
        }

        public void PrepareTestWrongAnswerRows(ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            //userI -  correct answer (after ranking period) 0 wrong answers

            //userH - 1 wrong answer and 1 correct answer (on different puzzles)
            dbContext.UserTestWrongAnswers.Add(new UserTestWrongAnswer()
            {
                Id = 11,
                UserId = UserModel.userH.Id,
                User = UserModel.userH,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                Time = this._testModel.test2.StartDate.Value.AddHours(1)
            });

            //userG - 2 wrong answers on same puzzle (2 correct answers) after 24h
            dbContext.UserTestWrongAnswers.Add(new UserTestWrongAnswer()
            {
                Id = 12,
                UserId = UserModel.userG.Id,
                User = UserModel.userG,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(1)
            });

            dbContext.UserTestWrongAnswers.Add(new UserTestWrongAnswer()
            {
                Id = 13,
                UserId = UserModel.userG.Id,
                User = UserModel.userG,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(25)
            });

            //userF - 0 wrong answer (0 correct answer)

            //userE - 1 wrong answer (0 correct answers)

            dbContext.UserTestWrongAnswers.Add(new UserTestWrongAnswer()
            {
                Id = 14,
                UserId = UserModel.userE.Id,
                User = UserModel.userE,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(1)
            });

            //userD - no wrong answers (2 correct answers) - answered few milliseconds earlier

            //userC - no wrong answers (2 correct answers) - answered few milliseconds later

            //userB - 2 wrong answers on same puzzle (2 correct answers)

            dbContext.UserTestWrongAnswers.Add(new UserTestWrongAnswer()
            {
                Id = 1,
                UserId = UserModel.userB.Id,
                User = UserModel.userB,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(1)
            });

            dbContext.UserTestWrongAnswers.Add(new UserTestWrongAnswer()
            {
                Id = 2,
                UserId = UserModel.userB.Id,
                User = UserModel.userB,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(1).AddMinutes(1)
            });

            //userA - 2 wrong answers on different puzzles (2 correct answers)

            dbContext.UserTestWrongAnswers.Add(new UserTestWrongAnswer()
            {
                Id = 3,
                UserId = UserModel.userA.Id,
                User = UserModel.userA,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                Time = this._testModel.test1.StartDate.Value.AddHours(1)
            });

            dbContext.UserTestWrongAnswers.Add(new UserTestWrongAnswer()
            {
                Id = 8,
                UserId = UserModel.userA.Id,
                User = UserModel.userA,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                Time = this._testModel.test2.StartDate.Value.AddHours(1)
            });
        }
    }
}
