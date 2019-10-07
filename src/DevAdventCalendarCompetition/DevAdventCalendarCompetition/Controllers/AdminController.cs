using System;
using System.Globalization;
using DevAdventCalendarCompetition.Models;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using DevExeptionsMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IBaseTestService baseTestService;

        public AdminController(IAdminService adminService, IBaseTestService baseTestService)
        {
            this._adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            this.baseTestService = baseTestService ?? throw new ArgumentNullException(nameof(baseTestService));
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
        public ActionResult AddTest(TestVm model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.ModelState.IsValid)
            {
                var dbTest = this.baseTestService.GetTestByNumber(model.Number);

                if (dbTest != null)
                {
                    this.ModelState.AddModelError(nameof(model.Number), "Test o podanym numerze już istnieje.");
                    return this.View(model);
                }

                // automatically set start and end time
                var testDay = model.StartDate;
                model.StartDate = testDay.AddHours(12).AddMinutes(00);
                model.EndDate = testDay.AddHours(23).AddMinutes(59);

                var testDto = new TestDto
                {
                    Number = model.Number,
                    Description = model.Description,
                    Answer = model.Answer.ToUpper(CultureInfo.InvariantCulture).Replace(" ", string.Empty, StringComparison.Ordinal),
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
                throw new ArgumentException(ExceptionsMessages.TestAlreadyRun);
            }

            var previousTestDto = this._adminService.GetPreviousTest(testDto.Number);
            if (previousTestDto != null && previousTestDto.Status != TestStatus.Ended)
            {
                throw new ArgumentException(ExceptionsMessages.PreviousTestIsNotDone);
            }

            this._adminService.UpdateTestDates(testDto, minutesString);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EndTest(int testId)
        {
            var testDto = this._adminService.GetTestById(testId);
            if (testDto.Status != TestStatus.Started)
            {
                throw new ArgumentException(ExceptionsMessages.TestAlreadyRun);
            }

            this._adminService.UpdateTestEndDate(testDto, DateTime.Now);

            return this.RedirectToAction("Index");
        }

        public string Reset()
        {
            var resetEnabledString = "true"; // TODO get from AppSettings // ConfigurationManager.AppSettings["ResetEnabled"];

            // TODO: move to service
#pragma warning disable CA1806 // Do not ignore method results
            bool.TryParse(resetEnabledString, out bool resetEnabled);
#pragma warning restore CA1806 // Do not ignore method results
            if (!resetEnabled)
            {
                return "Reset nie jest włączony.";
            }

            this._adminService.ResetTestDates();
            this._adminService.ResetTestAnswers();

            return "Dane zostały zresetowane.";
        }
    }
}