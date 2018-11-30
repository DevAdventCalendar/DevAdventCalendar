using DevAdventCalendarCompetition.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DevAdventCalendarCompetition.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IBaseTestService _baseTestService;

        public AdminController(IAdminService adminService, IBaseTestService baseTestService)
        {
            _adminService = adminService;
            _baseTestService = baseTestService;
        }

        public ActionResult Index()
        {
            var tests = _adminService.GetAllTests();

            return View(tests);
        }

        public ActionResult AddTest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTest(TestVm model)
        {
            if (ModelState.IsValid)
            {
                var dbTest = _baseTestService.GetTestByNumber(model.Number);
                if (dbTest != null)
                {
                    ModelState.AddModelError(nameof(model.Number), "Test o podanym numerze już istnieje.");
                    return View(model);
                }

                //automatically set start and end time
                var testDay = model.StartDate;
                model.StartDate = testDay.AddHours(12).AddMinutes(00);
                model.EndDate = testDay.AddHours(23).AddMinutes(59);

                var testDto = new TestDto
                {
                    Number = model.Number,
                    Description = model.Description,
                    Answer = model.Answer.ToUpper().Replace(" ", ""),
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    SponsorLogoUrl = model.SponsorLogoUrl,
                    SponsorName = model.SponsorName,
                    Discount = model.Discount
                };
                _adminService.AddTest(testDto);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult StartTest(int testId, string minutesString)
        {
            var testDto = _adminService.GetTestById(testId);
            if (testDto.Status != TestStatus.NotStarted)
                throw new ArgumentException("Test został uruchomiony");

            var previousTestDto = _adminService.GetPreviousTest(testDto.Number);
            if (previousTestDto != null && previousTestDto.Status != TestStatus.Ended)
                throw new ArgumentException("Poprzedni test nie został zakończony");

            _adminService.UpdateTestDates(testDto, minutesString);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EndTest(int testId)
        {
            var testDto = _adminService.GetTestById(testId);
            if (testDto.Status != TestStatus.Started)
                throw new ArgumentException("Test został uruchomiony");

            _adminService.UpdateTestEndDate(testDto, DateTime.Now);

            return RedirectToAction("Index");
        }

        public string Reset()
        {
            //TODO: move to service
            var resetEnabled = false;
            var resetEnabledString = "true"; // TODO get from AppSettings // ConfigurationManager.AppSettings["ResetEnabled"];
            bool.TryParse(resetEnabledString, out resetEnabled);
            if (!resetEnabled)
                return "Reset nie jest włączony.";

            _adminService.ResetTestDates();
            _adminService.ResetTestAnswers();

            return "Dane zostały zresetowane.";
        }
    }
}