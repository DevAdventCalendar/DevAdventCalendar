using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class Test1Controller : BaseTestController
    {
        public Test1Controller(IBaseTestService baseTestService) : base(baseTestService)
        {
        }

        [CanStartTest(TestNumber = 1)]
        public ActionResult Index()
        {
            var test = _baseTestService.GetTestByNumber(1);
            return View(test);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 1)]
        public ActionResult Index(string answer = "")
        {
            var fixedAnswer = answer.ToUpper().Replace(" ", "");

            if (fixedAnswer != "0100111101000010010010100100010101000011010101000100100101010110010010010101010001011001")
            {
                ModelState.AddModelError("", "Answer is not correct. Try again.");
                var test = _baseTestService.GetTestByNumber(1);
                return View("Index", test);
            }

            return SaveAnswerAndRedirect(1);
        }
    }
}