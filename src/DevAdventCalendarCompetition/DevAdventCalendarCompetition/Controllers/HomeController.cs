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
                                Offset = ta.AnsweringTimeOffset
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
            List<TotalTestResultEntryVm> totalTestResults = null;

            if (testDtoList.All(t => t.HasEnded))
            {
                var totalResultsDict = singleTestResults
                    .SelectMany(r => r.Entries)
                    .Select(r => new TotalTestResultEntryVm { FullName = r.FullName, UserId = r.UserId })
                    .Distinct(new TotalTestResultEntryVmComparer())
                    .ToDictionary(el => el.UserId);

                foreach (var singleTestResult in singleTestResults)
                {
                    var userCountedDict = new Dictionary<string, bool>();
                    singleTestResult.Entries.ForEach(e =>
                    {
                        var totalResult = totalResultsDict[e.UserId];
                        totalResult.TotalPoints += e.Points;
                        totalResult.TotalOffset += e.Offset;
                        userCountedDict[e.UserId] = true;
                    });

                    foreach (var entry in totalResultsDict)
                    {
                        if (userCountedDict.ContainsKey(entry.Key) == false)
                        {
                            entry.Value.TotalOffset += singleTestResult.TestOffset;
                        }
                    }
                }

                totalTestResults = totalResultsDict
                    .Values.ToList()
                    .OrderByDescending(t => t.TotalPoints)
                    .ThenBy(t => t.TotalOffset)
                    .ToList();
            }
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