using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Vms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ActionResult SaveAnswerAndRedirect(int testNumber)
        {
            var test = _baseTestService.GetTestByNumber(testNumber);
            //TODO: check for null, error handling

            _baseTestService.AddTestAnswer(test.Id, User.FindFirstValue(ClaimTypes.NameIdentifier), test.StartDate.Value);

            //TODO: use Automapper?
            var testAnswer = _baseTestService.GetAnswerByTestId(test.Id);

            var testAnswerVm = new TestAnswerVm()
            {
                TestId = testAnswer.TestId,
                UserId = testAnswer.UserId,
                AnsweringTime = testAnswer.AnsweringTime,
                AnsweringTimeOffset = testAnswer.AnsweringTimeOffset
            };

            var answerVm = new AnswerVm() { TestAnswerVm = testAnswerVm, TestNumber = testNumber };

            return View("Answered", answerVm);
        }
    }
}