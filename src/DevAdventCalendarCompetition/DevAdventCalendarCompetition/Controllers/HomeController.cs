using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DevAdventCalendarCompetition.Models;
using DevAdventCalendarCompetition.Data;
using DevAdventCalendarCompetition.Vms;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DevAdventCalendarCompetition.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var tests = _context.Set<Test>().ToList();
            var currentTest = tests.FirstOrDefault(el => el.Status == TestStatus.Started);
            if (currentTest == null)
                return View();

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return View(currentTest);

            var currentTestAnswer =
                _context.Set<TestAnswer>().FirstOrDefault(el => el.UserId == userId && el.TestId == currentTest.Id);
            if (currentTestAnswer == null)
                ViewBag.TestNotAnswered = true;

            return View(currentTest);
        }

        public ActionResult Results()
        {
            var tests = _context.Set<Test>()
                .Include(t => t.Answers)
                .ThenInclude(ta => ta.User)
                .OrderBy(el => el.Number).ToList();

            var singleTestResults = tests.Select(test => new SingleTestResultsVm()
            {
                TestNumber = test.Number,
                TestEnded = test.HasEnded,
                EndDate = test.EndDate,
                StartDate = test.StartDate,
                Entries = test.Answers.OrderBy(ta => ta.AnsweringTimeOffset)
                    .Select(
                        ta =>
                            new SingleTestResultEntry()
                            {
                                UserId = ta.UserId,
                                FullName = ta.User.FirstName + " " + ta.User.SecondName,
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

            if (tests.All(t => t.HasEnded))
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

        public ActionResult Error()
        {
            ViewBag.errorMessage = TempData["errorMessage"];
            return View();
        }

        public ActionResult About()
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
