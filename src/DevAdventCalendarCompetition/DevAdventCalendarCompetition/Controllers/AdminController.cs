using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using DevAdventCalendarCompetition.Models.Test;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Resources;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ITestService _testService;
        private readonly TestSettings _testSettings;

        public AdminController(IAdminService adminService, ITestService testService, TestSettings testSettings)
        {
            this._adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            this._testService = testService ?? throw new ArgumentNullException(nameof(testService));
            this._testSettings = testSettings;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var tests = this._adminService.GetAllTests();
            this.ViewBag.DateTime = DateTime.Now;
            this.ViewBag.DateTimeUtc = DateTime.UtcNow;

            return this.View(tests);
        }

        [HttpGet]
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
                model = this.SetHours(model);

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

        private TestViewModel SetHours(TestViewModel model)
        {
            model.StartDate = model.StartDate.AddTicks(this._testSettings.StartHour.Ticks);
            model.EndDate = model.EndDate.AddTicks(this._testSettings.EndHour.Ticks);
            return model;
        }
    }
}