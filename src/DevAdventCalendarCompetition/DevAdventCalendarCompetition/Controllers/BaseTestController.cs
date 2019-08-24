using System;
using System.Security.Claims;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Vms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize]
    public class BaseTestController : Controller
    {
        private readonly IBaseTestService baseTestService;

        public BaseTestController(IBaseTestService baseTestService)
        {
            this.baseTestService = baseTestService;
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

            var testAnswerVm = new TestAnswerVm()
            {
                TestId = testAnswerDto.TestId,
                UserId = testAnswerDto.UserId,
                AnsweringTime = testAnswerDto.AnsweringTime,
                AnsweringTimeOffset = testAnswerDto.AnsweringTimeOffset
            };

            var answerVm = new AnswerVm() { TestAnswerVm = testAnswerVm, TestNumber = testNumber };

            return this.View("Answered", answerVm);
        }
    }
}