using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DevAdventCalendarCompetition.Resources.ViewsMessages;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            this._testService = testService;
        }

        [HttpGet]
        public ActionResult Index(int testNumber)
        {
            var test = this._testService.GetTestByNumber(testNumber);

            if (test == null)
            {
                return this.NotFound();
            }

            var userHasAnswered = this._testService.HasUserAnsweredTest(this.User.FindFirstValue(ClaimTypes.NameIdentifier), test.Id);
            if (userHasAnswered)
            {
                test.HasUserAnswered = true;
                return this.View(test);
            }

            return this.View(test);
        }

        [HttpPost]
        public ActionResult Index(int testNumber, string answer = "")
        {
            var test = this._testService.GetTestByNumber(testNumber);

            if (string.IsNullOrWhiteSpace(answer))
            {
                this.ModelState.AddModelError("Answers", @GiveUsYourAnswer);
                return this.View("Index", test);
            }

            if (test == null)
            {
                return this.NotFound();
            }

            var finalAnswer = this.PrepareAnswer(answer);

            if (this._testService.HasUserAnsweredTest(this.User.FindFirstValue(ClaimTypes.NameIdentifier), test.Id))
            {
                test.HasUserAnswered = true;
                return this.View("Index", test);
            }

            if (test.Status == TestStatus.Ended || test.Status == TestStatus.NotStarted)
            {
                this.ModelState.AddModelError("Answers", @ThereIsError);
                return this.View("Index", test);
            }

            if (this._testService.VerifyTestAnswer(finalAnswer, test.Answers.Select(t => t.Answer).ToList()))
            {
                var answerViewModel = this.SaveAnswer(test);
                return this.View("TestAnswered", answerViewModel);
            }

            this.SaveWrongAnswer(test.Id, finalAnswer);
            test.UserAnswer = finalAnswer;
            this.ModelState.AddModelError("Answers", @WrongTryAgain);

            return this.View("Index", test);
        }

        private void SaveWrongAnswer(int testId, string wrongAnswer)
        {
            this._testService.AddTestWrongAnswer(this.User.FindFirstValue(ClaimTypes.NameIdentifier), testId, wrongAnswer, DateTime.Now);
        }

        // I don't get this error. It complains about making this method as static but this can't be static, it's an instance method...
#pragma warning disable CA1822 // Mark members as static
        private string PrepareAnswer(string userAnswer) => Regex.Replace(userAnswer.ToUpper(CultureInfo.CurrentCulture).Trim(), "\\s{2,}", " ");
#pragma warning restore CA1822 // Mark members as static

        private AnswerViewModel SaveAnswer(TestDto testDto)
        {
            // TODO: check for null, error handling
            this._testService.AddTestAnswer(testDto.Id, this.User.FindFirstValue(ClaimTypes.NameIdentifier), testDto.StartDate.Value);

            // TODO: use Automapper?
            var testAnswerDto = this._testService.GetAnswerByTestId(testDto.Id);

            var testAnswerViewModel = new TestAnswerViewModel()
            {
                TestId = testAnswerDto.TestId,
                UserId = testAnswerDto.UserId,
                AnsweringTime = testAnswerDto.AnsweringTime,
                AnsweringTimeOffset = testAnswerDto.AnsweringTimeOffset
            };

            return new AnswerViewModel() { TestAnswerViewModel = testAnswerViewModel, TestNumber = testDto.Number };
        }
    }
}