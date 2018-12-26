using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Vms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DevAdventCalendarCompetition.Controllers
{
    public class HomeController : Controller
    {
        protected IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public ActionResult Index()
        {
            var currentTestsDto = _homeService.GetCurrentTests();
            if (currentTestsDto == null)
                return View();

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return View(currentTestsDto);

            ViewBag.CorrectAnswers = _homeService.GetCorrectAnswersCountForUser(userId);

            return View(currentTestsDto);
        }

        public ActionResult Results()
        {           
            var testDtoList = _homeService.GetTestsWithUserAnswers();

            var singleTestResults = testDtoList.Select(testDto => new SingleTestResultsVm()
            {
                TestNumber = testDto.Number,
                TestEnded = testDto.HasEnded,
                EndDate = testDto.EndDate,
                StartDate = testDto.StartDate,
                Entries = testDto.Answers.OrderBy(ta => ta.AnsweringTimeOffset)
                    .Select(
                        ta =>
                            new SingleTestResultEntry()
                            {
                                UserId = ta.UserId,
                                FullName = ta.UserFullName,
                                CorrectAnswersCount = 0,
                                WrongAnswersCount = 0
                            })
                            .ToList()
            }).ToList();

            singleTestResults.ForEach(r =>
            {
                for (int i = 0; i < r.Entries.Count; i++)
                {
                    if (i >= 30)
                        continue;

                    var entry = r.Entries[i];
                    entry.Points = PointsPerPlace[i];
                }
            });

            List<TotalTestResultEntryVm> totalTestResults = new List<TotalTestResultEntryVm>();

            totalTestResults.Add(new TotalTestResultEntryVm
            {
                UserId = 1,
                FullName = "devadventcalendar@gmail.com",
                TotalPoints = 500
            });

            var vm = new TestResultsVm()
            {
                SingleTestResults = singleTestResults,
                TotalTestResults = totalTestResults
            };

            return View("Results", vm);
        }

        [HttpPost]
        public ActionResult CheckTestStatus(int testNumber)
        {
            return Content(_homeService.CheckTestStatus(testNumber));  
        }

        public ActionResult Error()
        {
            ViewBag.errorMessage = TempData["errorMessage"];
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Sponsors()
        {
            return View();
        }

        public ActionResult Prizes()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult TestHasNotStarted(int number)
        {
            return View(number);
        }

        public ActionResult TestHasEnded(int number)
        {
            return View(number);
        }

        public ActionResult TestAnswered(int number)
        {
            return View(number);
        }

        public ActionResult GetServerTime()
        {
            return Json(DateTime.Now.ToString("yyyy'-'MM'-'ddTHH':'mm':'ss.fff%K"));
        }

        private int[] PointsPerPlace =
        {
            100, 80, 60, 50, 45, 40, 36, 32, 29, 26, 24, 22, 20, 18, 16, 15, 14, 13, 12, 11,
            10, 9, 8, 7, 6, 5, 4, 3, 2, 1
        };
    }
}