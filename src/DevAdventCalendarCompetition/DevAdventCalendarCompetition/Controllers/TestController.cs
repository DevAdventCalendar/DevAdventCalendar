using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class TestController : BaseTestController
    {
        public TestController(IBaseTestService baseTestService) : base(baseTestService)
        {
        }

        [HttpGet]
        public ActionResult Index(int testNumber)
        {
            var test = _baseTestService.GetTestByNumber(testNumber);

            return View(test);
        }

        [HttpPost]
        public ActionResult Index(int testNumber, string answer = "")
        {
            var test = _baseTestService.GetTestByNumber(testNumber);
            var finalAnswer = answer.ToUpper().Replace(" ", "");

            if (test != null && !_baseTestService.VerifyTestAnswer(finalAnswer, test.Answer))
            {
                SaveWrongAnswer(finalAnswer, testNumber);

                ModelState.AddModelError("", "Źle! Spróbuj ponownie :)");

                return View("Index", test);
            }

            return SaveAnswerAndRedirect(testNumber);
        }
    }
}