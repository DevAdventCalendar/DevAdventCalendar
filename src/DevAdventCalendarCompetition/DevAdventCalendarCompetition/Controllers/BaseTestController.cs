using System;
using System.Security.Claims;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize]
    public class BaseTestController : Controller
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable SA1401 // Fields should be private
        protected readonly IBaseTestService baseTestService;
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA1051 // Do not declare visible instance fields

        public BaseTestController(IBaseTestService baseTestService)
        {
            this.baseTestService = baseTestService ?? throw new ArgumentNullException(nameof(baseTestService));
        }

        public void SaveWrongAnswer(string wrongAnswer, int testNumber)
        {
            var testDto = this.baseTestService.GetTestByNumber(testNumber);

            this.baseTestService.AddTestWrongAnswer(this.User.FindFirstValue(ClaimTypes.NameIdentifier), testDto.Id, wrongAnswer, DateTime.Now);
        }

        public ActionResult SaveAnswerAndRedirect(int testNumber)
        {
            var testDto = this.baseTestService.GetTestByNumber(testNumber);

            // TODO: check for null, error handling
            this.baseTestService.AddTestAnswer(testDto.Id, this.User.FindFirstValue(ClaimTypes.NameIdentifier), testDto.StartDate.Value);

            // TODO: use Automapper?
            var testAnswerDto = this.baseTestService.GetAnswerByTestId(testDto.Id);

            var testAnswerViewModel = new TestAnswerViewModel()
            {
                TestId = testAnswerDto.TestId,
                UserId = testAnswerDto.UserId,
                AnsweringTime = testAnswerDto.AnsweringTime,
                AnsweringTimeOffset = testAnswerDto.AnsweringTimeOffset
            };

            var answerViewModel = new AnswerViewModel() { TestAnswerViewModel = testAnswerViewModel, TestNumber = testNumber };

            return this.View("TestAnswered", answerViewModel);
        }
    }
}
