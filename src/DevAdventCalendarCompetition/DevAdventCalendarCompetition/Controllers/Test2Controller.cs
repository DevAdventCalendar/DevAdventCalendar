using DevAdventCalendarCompetition.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class Test2Controller : BaseTestController
    {
        public Test2Controller(IBaseTestService baseTestService) : base(baseTestService)
        {
        }

        [CanStartTest(TestNumber = 2)]
        public ActionResult Index()
        {
            var test = _baseTestService.GetTestByNumber(2);

            return View(test);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 2)]
        public ActionResult Index(string answer = "")
        {
            var fixedAnswer = answer.ToUpper().Replace(" ", "");

            if (fixedAnswer != "ANVQOFUHUFKUESDQMF")
            {
                ModelState.AddModelError("", "Answer is not correct. Try again.");

                var test = _baseTestService.GetTestByNumber(2);
                return View("Index", test);
            }

            return SaveAnswerAndRedirect(2);
        }
    }
}