using System;
using System.Globalization;
using DevAdventCalendarCompetition.Models;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly IBaseTestService baseTestService;

        public AdminController(IAdminService adminService, IBaseTestService baseTestService)
        {
            this.adminService = adminService;
            this.baseTestService = baseTestService;
        }

        public ActionResult Index()
        {
            var tests = this.adminService.GetAllTests();
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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));

            }

            if (model != null)
            {
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
                        Answer = model.Answer.ToUpper(CultureInfo.InvariantCulture).Replace(" ", " ", StringComparison.Ordinal),
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        SponsorLogoUrl = model.SponsorLogoUrl,
                        SponsorName = model.SponsorName,
                        Discount = model.Discount,
                        DiscountUrl = model.DiscountUrl,
                        DiscountLogoUrl = model.DiscountLogoUrl,
                        DiscountLogoPath = model.DiscountLogoPath
                    };
                    this.adminService.AddTest(testDto);
                    return this.RedirectToAction("Index");
                }

                return this.View(model);
               }
        }

        [HttpPost]
        public ActionResult StartTest(int testId, string minutesString)
        {
            var testDto = this.adminService.GetTestById(testId);
            if (testDto.Status != TestStatus.NotStarted)
            {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                throw new ArgumentException("Test został uruchomiony");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }

            var previousTestDto = this.adminService.GetPreviousTest(testDto.Number);
            if (previousTestDto != null && previousTestDto.Status != TestStatus.Ended)
            {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                throw new ArgumentException("Poprzedni test nie został zakończony");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }

            this.adminService.UpdateTestDates(testDto, minutesString);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EndTest(int testId)
        {
            var testDto = this.adminService.GetTestById(testId);
            if (testDto.Status != TestStatus.Started)
            {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                throw new ArgumentException("Test został uruchomiony");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }

            this.adminService.UpdateTestEndDate(testDto, DateTime.Now);

            return this.RedirectToAction("Index");
        }

        public string Reset()
        {
            // TODO: move to service
            var resetEnabled = false;
            var resetEnabledString = "true"; // TODO get from AppSettings // ConfigurationManager.AppSettings["ResetEnabled"];
#pragma warning disable CA1806 // Do not ignore method results
            bool.TryParse(resetEnabledString, out resetEnabled);
#pragma warning restore CA1806 // Do not ignore method results
            if (!resetEnabled)
            {
                return "Reset nie jest włączony.";
            }

            this.adminService.ResetTestDates();
            this.adminService.ResetTestAnswers();

            return "Dane zostały zresetowane.";
        }
    }
}