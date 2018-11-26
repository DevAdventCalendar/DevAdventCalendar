using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Vms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize]
    public class BaseTestController : Controller
    {
        protected readonly IBaseTestService _baseTestService;

        public BaseTestController(IBaseTestService baseTestService)
        {
            _baseTestService = baseTestService;
        }

        public void SaveWrongAnswer(string wrongAnswer, int testNumber)
        {
            var testDto = _baseTestService.GetTestByNumber(testNumber);

            _baseTestService.AddTestWrongAnswer(User.FindFirstValue(ClaimTypes.NameIdentifier), testDto.Id, wrongAnswer, DateTime.Now);
        }

        public ActionResult SaveAnswerAndRedirect(int testNumber)
        {
            var testDto = _baseTestService.GetTestByNumber(testNumber);

            //TODO: check for null, error handling

            _baseTestService.AddTestAnswer(testDto.Id, User.FindFirstValue(ClaimTypes.NameIdentifier), testDto.StartDate.Value);

            //TODO: use Automapper?
            var testAnswerDto = _baseTestService.GetAnswerByTestId(testDto.Id);

            var testAnswerVm = new TestAnswerVm()
            {
                TestId = testAnswerDto.TestId,
                UserId = testAnswerDto.UserId,
                AnsweringTime = testAnswerDto.AnsweringTime,
                AnsweringTimeOffset = testAnswerDto.AnsweringTimeOffset
            };

            var answerVm = new AnswerVm() { TestAnswerVm = testAnswerVm, TestNumber = testNumber };

            return View("Answered", answerVm);
        }
    }
}