using DevAdventCalendarCompetition.Data;
using DevAdventCalendarCompetition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var tests = _context.Set<Test>().OrderBy(t => t.Number).ToList();

            return View(tests);
        }

        [HttpPost]
        public ActionResult StartTest(int testId, string minutesString)
        {
            var test = _context.Set<Test>().First(el => el.Id == testId);
            if (test.Status != TestStatus.NotStarted)
                throw new ArgumentException("Test was started");

            var previousTest = _context.Set<Test>().FirstOrDefault(el => el.Number == test.Number - 1);

            if (previousTest != null && previousTest.Status != TestStatus.Ended)
                throw new ArgumentException("Previous test has not ended");

            uint minutes = 0;
            var parsed = uint.TryParse(minutesString, out minutes);
            if (!parsed)
                minutes = 20;

            var startTime = DateTime.Now;
            var endTime = startTime.AddMinutes(minutes);
            test.StartDate = startTime;
            test.EndDate = endTime;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EndTest(int testId)
        {
            var test = _context.Set<Test>().First(el => el.Id == testId);
            if (test.Status != TestStatus.Started)
                throw new ArgumentException("Test was started");

            var startTime = DateTime.Now;
            test.EndDate = startTime;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public string Reset()
        {
            var resetEnabled = false;
            var resetEnabledString = "true"; // TODO get from AppSettings // ConfigurationManager.AppSettings["ResetEnabled"];
            bool.TryParse(resetEnabledString, out resetEnabled);
            if (!resetEnabled)
                return "Reset is not enabled.";

            var tests = _context.Set<Test>().ToList();
            foreach (var test in tests)
            {
                test.StartDate = null;
                test.EndDate = null;
            }

            var testAnswers = _context.Set<TestAnswer>().ToList();
            _context.Set<TestAnswer>().RemoveRange(testAnswers);

            _context.SaveChanges();

            return "Data was reseted.";
        }
    }
}