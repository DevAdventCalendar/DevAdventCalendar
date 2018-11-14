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

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
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
                var testDto = new TestDto
                {
                    Number = model.Number,
                    Description = model.Description,
                    Answer = model.Answer,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    SponsorLogoUrl = model.SponsorLogoUrl,
                    SponsorName = model.SponsorName
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
                throw new ArgumentException("Test was started");

            var previousTestDto = _adminService.GetPreviousTest(testDto.Number);
            if (previousTestDto != null && previousTestDto.Status != TestStatus.Ended)
                throw new ArgumentException("Previous test has not ended");

            //TODO: move to service
            uint minutes = 0;
            var parsed = uint.TryParse(minutesString, out minutes);
            if (!parsed)
                minutes = 20;

            var startTime = DateTime.Now;
            var endTime = startTime.AddMinutes(minutes);

            _adminService.UpdateTestDates(testDto, startTime, endTime);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EndTest(int testId)
        {
            var testDto = _adminService.GetTestById(testId);
            if (testDto.Status != TestStatus.Started)
                throw new ArgumentException("Test was started");

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
                return "Reset is not enabled.";

            _adminService.ResetTestDates();
            _adminService.ResetTestAnswers();

            return "Data was reseted.";
        }
    }
}