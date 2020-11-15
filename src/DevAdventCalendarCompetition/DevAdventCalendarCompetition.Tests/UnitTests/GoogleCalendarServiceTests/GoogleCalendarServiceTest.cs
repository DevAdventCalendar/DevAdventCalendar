using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Models.GoogleCalendar;
using DevAdventCalendarCompetition.Services.Options;
using Newtonsoft.Json;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests.GoogleCalendarServiceTests
{
    public class GoogleCalendarServiceTest
    {
        private readonly GoogleCalendarSettings _googleCalendarSettings = new GoogleCalendarSettings()
        {
            Summary = "TestSummary",
            Events = new CalendarEvent()
            {
                Location = "Warsaw",
                ReminderMethod = "null",
                ReminderMinutes = 11,
                Summary = "TestEvent",
                TimeZone = "Warsaw"
            },
            CalendarsEndpoint = "https://www.googleapis.com/calendar/v3/calendars",
            EventsEndpoint = "https://www.googleapis.com/calendar/v3/calendars/{0}/events"
        };

        private readonly AdventSettings _adventSettings = new AdventSettings()
        {
            StartDate = new DateTime(2020, 12, 1),
            EndDate = new DateTime(2020, 12, 24)
        };

        private readonly TestSettings _testSettings = new TestSettings()
        {
            StartHour = new TimeSpan(12, 1, 0),
            EndHour = new TimeSpan(12, 0, 0)
        };

        [Fact]
        public async Task CreateCalendarWithEventsShouldReturnOperationalResultCalendarFailure()
        {
            var messages = new Dictionary<string, HttpResponseMessage>()
            {
                [this._googleCalendarSettings.CalendarsEndpoint] = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                },
                [this._googleCalendarSettings.EventsEndpoint] = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                }
            };

            using var testMessageHandler = new TestMessageHandler(messages);
            using (var httpClient = new HttpClient(testMessageHandler))
            {
                var googleCalendarService = new GoogleCalendarService(
                        httpClient,
                        this._adventSettings,
                        this._testSettings,
                        this._googleCalendarSettings);

                // Act
                var createdCalendar = await googleCalendarService.CreateNewCalendarWithEvents().ConfigureAwait(false);

                // Assert
                Assert.IsType<OperationalResult>(createdCalendar);
                Assert.Equal(OperationalResultStatus.CalendarFailure, createdCalendar.Status);
            }
        }

        [Fact]
        public async Task CreateCalendarWithEventsShouldReturnOperationalResultEventFailure()
        {
            var newCalendarId = "1";
            var eventsUrl = this.GetEventsUrl(newCalendarId);
            var messages = new Dictionary<string, HttpResponseMessage>()
            {
                [this._googleCalendarSettings.CalendarsEndpoint] = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(new CalendarDto() { Id = newCalendarId }),
                        Encoding.UTF8, MediaTypeNames.Application.Json)
                },
                [eventsUrl] = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                }
            };

            using var testMessageHandler = new TestMessageHandler(messages);
            using var httpClient = new HttpClient(testMessageHandler);
            var googleCalendarService = new GoogleCalendarService(
                httpClient,
                this._adventSettings,
                this._testSettings,
                this._googleCalendarSettings);

            // Act
            var createdCalendar = await googleCalendarService.CreateNewCalendarWithEvents().ConfigureAwait(false);

            // Assert
            Assert.IsType<OperationalResult>(createdCalendar);
            Assert.Equal(OperationalResultStatus.EventsFailure, createdCalendar.Status);
        }

        [Fact]
        public async Task CreateCalendarWithEventsShouldReturnOperationalResultSuccess()
        {
            // Arrange
            var newCalendarId = "1";
            var eventsUrl = this.GetEventsUrl(newCalendarId);
            var messages = new Dictionary<string, HttpResponseMessage>()
            {
                [this._googleCalendarSettings.CalendarsEndpoint] = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(new CalendarDto() { Id = newCalendarId }),
                        Encoding.UTF8, MediaTypeNames.Application.Json)
                },
                [eventsUrl] = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                }
            };

            using var testMessageHandler = new TestMessageHandler(messages);
            using var httpClient = new HttpClient(testMessageHandler);
            var googleCalendarService = new GoogleCalendarService(
                httpClient,
                this._adventSettings,
                this._testSettings,
                this._googleCalendarSettings);

            // Act
            var createdCalendar = await googleCalendarService.CreateNewCalendarWithEvents().ConfigureAwait(false);

            // Assert
            Assert.IsType<OperationalResult>(createdCalendar);
            Assert.Equal(OperationalResultStatus.Success, createdCalendar.Status);
        }

        private string GetEventsUrl(string calendarId) => string.Format(
            CultureInfo.InvariantCulture,
            this._googleCalendarSettings.EventsEndpoint,
            calendarId);
    }
}
