using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using DevAdventCalendarCompetition.Models;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Resources;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ITestService _testService;

        public AdminController(IAdminService adminService, ITestService testService)
        {
            this._adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            this._testService = testService ?? throw new ArgumentNullException(nameof(testService));
        }

        public ActionResult Index()
        {
            var tests = this._adminService.GetAllTests();
            this.ViewBag.DateTime = DateTime.Now;
            this.ViewBag.DateTimeUtc = DateTime.UtcNow;

            return this.View(tests);
        }

        public ActionResult AddTest()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AddTest(TestViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.ModelState.IsValid)
            {
                var dbTest = this._testService.GetTestByNumber(model.Number);

                if (dbTest != null)
                {
                    this.ModelState.AddModelError("Number", ExceptionsMessages.TestAlreadyExists);
                    return this.View(model);
                }

                // automatically set start and end time
                model.StartDate = model.StartDate.AddHours(20).AddMinutes(00);
                model.EndDate = model.EndDate.AddHours(23).AddMinutes(59);

                var answers = model.Answers.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => new TestAnswerDto()
                {
                    Answer = a.ToUpper(CultureInfo.InvariantCulture)
                        .Replace(" ", string.Empty, StringComparison.Ordinal)
                }).ToList();

                var testDto = new TestDto
                {
                    Number = model.Number,
                    Description = model.Description,
                    Answers = answers,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    SponsorLogoUrl = model.SponsorLogoUrl,
                    SponsorName = model.SponsorName,
                    Discount = model.Discount,
                    DiscountUrl = model.DiscountUrl,
                    DiscountLogoUrl = model.DiscountLogoUrl,
                    DiscountLogoPath = model.DiscountLogoPath
                };
                this._adminService.AddTest(testDto);
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpPost]
        public ActionResult StartTest(int testId, string minutesString)
        {
            var testDto = this._adminService.GetTestById(testId);
            if (testDto.Status != TestStatus.NotStarted)
            {
                throw new InvalidOperationException(ExceptionsMessages.TestAlreadyRun);
            }

            var previousTestDto = this._adminService.GetPreviousTest(testDto.Number);
            if (previousTestDto != null && previousTestDto.Status != TestStatus.Ended)
            {
                throw new InvalidOperationException(ExceptionsMessages.PreviousTestIsNotDone);
            }

            this._adminService.UpdateTestDates(testDto.Id, minutesString);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EndTest(int testId)
        {
            var testDto = this._adminService.GetTestById(testId);
            if (testDto.Status != TestStatus.Started)
            {
                throw new InvalidOperationException(ExceptionsMessages.TestAlreadyRun);
            }

            this._adminService.UpdateTestEndDate(testDto.Id, DateTime.Now);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CalculateResults(int weekNumber)
        {
            if (weekNumber < 1 || weekNumber > 4)
            {
                return this.BadRequest("Błędny numer tygodnia.");
            }

            Process.Start(@"c:\\Windows\\System32\\cmd.exe", weekNumber.ToString(CultureInfo.CurrentCulture.DateTimeFormat));
            return this.Ok();
        }
    }
}