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
        private readonly TestModel _testModel;

        internal TestAnswerModel(TestModel testModel)
        {
            this._testModel = testModel;
        }

        public void PrepareTestAnswerRows(ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 16,
                UserId = UserModel.userI.Id,
                User = UserModel.userI,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddDays(8).AddHours(2).AddMilliseconds(5),
                AnsweringTimeOffset = new TimeSpan(8, 2, 0, 0, 5)
            });

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 17,
                UserId = UserModel.userI.Id,
                User = UserModel.userI,
                TestId = this._testModel.test7.Id,
                Test = this._testModel.test7,
                AnsweringTime = this._testModel.test7.StartDate.Value.AddDays(1).AddHours(2),
                AnsweringTimeOffset = new TimeSpan(1, 2, 0, 0)
            });

            //userI - 1 correct answer (after ranking time)
            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 15,
                UserId = UserModel.userI.Id,
                User = UserModel.userI,
                TestId = this._testModel.test8.Id,
                Test = this._testModel.test8,
                AnsweringTime = this._testModel.test8.StartDate.Value.AddMinutes(1),
                AnsweringTimeOffset = new TimeSpan(0, 1, 0)
            });


            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 14,
                UserId = UserModel.userI.Id,
                User = UserModel.userI,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddDays(8),
                AnsweringTimeOffset = new TimeSpan(8, 0, 0, 0)
            });

            //userH - 1 correct answer

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 13,
                UserId = UserModel.userH.Id,
                User = UserModel.userH,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddMinutes(10),
                AnsweringTimeOffset = new TimeSpan(0, 10, 0)
            });

            //userG - 2 correct answers

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 12,
                UserId = UserModel.userG.Id,
                User = UserModel.userG,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddDays(1).AddHours(2),
                AnsweringTimeOffset = new TimeSpan(1,2, 0, 0)
            });

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 11,
                UserId = UserModel.userG.Id,
                User = UserModel.userG,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                AnsweringTime = this._testModel.test2.StartDate.Value.AddMinutes(10),
                AnsweringTimeOffset = new TimeSpan(0, 10, 0)
            });

            //userF - 0 correct answers

            //userE - 0 correct answers

            //userD

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 1,
                UserId = UserModel.userD.Id,
                User = UserModel.userD,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddMinutes(10),
                AnsweringTimeOffset = new TimeSpan(0, 10, 0)
            });

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 2,
                UserId = UserModel.userD.Id,
                User = UserModel.userD,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                AnsweringTime = this._testModel.test2.StartDate.Value.AddMinutes(5),
                AnsweringTimeOffset = new TimeSpan(0, 5, 0)
            });

            //userC

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 3,
                UserId = UserModel.userC.Id,
                User = UserModel.userC,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddMinutes(10),
                AnsweringTimeOffset = new TimeSpan(0, 10, 0)
            });

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 4,
                UserId = UserModel.userC.Id,
                User = UserModel.userC,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                AnsweringTime = this._testModel.test2.StartDate.Value.AddMinutes(15),
                AnsweringTimeOffset = new TimeSpan(0, 15, 0)
            });

            //userB

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 5,
                UserId = UserModel.userB.Id,
                User = UserModel.userB,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddHours(2),
                AnsweringTimeOffset = new TimeSpan(2, 0, 0)
            });

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 6,
                UserId = UserModel.userB.Id,
                User = UserModel.userB,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                AnsweringTime = this._testModel.test2.StartDate.Value.AddHours(2).AddMilliseconds(10),
                AnsweringTimeOffset = new TimeSpan(0, 2, 0, 0, 10)
            });

            //userA

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 7,
                UserId = UserModel.userA.Id,
                User = UserModel.userA,
                TestId = this._testModel.test1.Id,
                Test = this._testModel.test1,
                AnsweringTime = this._testModel.test1.StartDate.Value.AddHours(2).AddMilliseconds(5),
                AnsweringTimeOffset = new TimeSpan(0, 2, 0, 0, 5)
            });

            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 8,
                UserId = UserModel.userA.Id,
                User = UserModel.userA,
                TestId = this._testModel.test2.Id,
                Test = this._testModel.test2,
                AnsweringTime = this._testModel.test2.StartDate.Value.AddHours(2),
                AnsweringTimeOffset = new TimeSpan(2, 0, 0)
            });
        }
    }
}
