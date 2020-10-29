using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Models.GoogleCalendar;
using DevAdventCalendarCompetition.Services.Options;
using Newtonsoft.Json;

namespace DevAdventCalendarCompetition.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly HttpClient _httpClient;
        private readonly AdventSettings _adventSettings;
        private readonly TestSettings _testSettings;
        private readonly GoogleCalendarSettings _calendarSettings;

        public GoogleCalendarService(
            HttpClient httpClient,
            AdventSettings adventSettings,
            TestSettings testSettings,
            GoogleCalendarSettings calendarSettings)
        {
            this._httpClient = httpClient;
            this._adventSettings = adventSettings;
            this._testSettings = testSettings;
            this._calendarSettings = calendarSettings;
        }

        public async Task<OperationalResult> CreateNewCalendarWithEvents()
        {
            var calendarResponse = await this.CreateCalendar();
            if (!calendarResponse.IsSuccessStatusCode)
            {
                return OperationalResult.Failure(OperationalResultStatus.CalendarFailure); // test 1 sprawdzic czy zwraca failure callendar jeden mock httpclient
            }

            var newCalendar = await calendarResponse.Content.ReadFromJsonAsync<CalendarDto>();
            var eventsResponse = await this.CreateEvents(newCalendar.Id);

            if (eventsResponse.IsSuccessStatusCode)
            {
                return OperationalResult.Success(); // test 1 czy zwraca succes dwa mocki
            }

            return OperationalResult.Failure(OperationalResultStatus.EventsFailure); // test 1 czy failure event dwa mocki
        }

        private async Task<HttpResponseMessage> CreateCalendar()
        {
            var newCalendar = new NewCalendarDto
            {
                Summary = this._calendarSettings.Summary
            };

            using (var content = new StringContent(JsonConvert.SerializeObject(newCalendar), Encoding.UTF8, MediaTypeNames.Application.Json))
            {
                var uri = new System.Uri(@"https://www.googleapis.com/calendar/v3/calendars");
                var response = await this._httpClient.PostAsync(uri, content);
                return response;
            }

            // return await this._httpClient.PostAsJsonAsync("calendars", newCalendar);
        }

        private async Task<HttpResponseMessage> CreateEvents(string calendarId)
        {
            var summary = this._calendarSettings.Events.Summary;
            var location = this._calendarSettings.Events.Location;
            var timeZone = this._calendarSettings.Events.TimeZone;
            var reminderMethod = this._calendarSettings.Events.ReminderMethod;
            var reminderMinutes = this._calendarSettings.Events.ReminderMinutes;
            var startDate = this._adventSettings.StartDate.AddTicks(this._testSettings.StartHour.Ticks);
            var endDate = this._adventSettings.EndDate;
            var daysCount = (endDate.Day - startDate.Day) + 1;
            var recurrence = $"RRULE:FREQ=DAILY;COUNT={daysCount}";

            var newEvents = new EventsDto
            {
                Summary = summary,
                Location = location,
                Start = new EventDate
                {
                    DateTime = startDate,
                    TimeZone = timeZone
                },
                End = new EventDate
                {
                    DateTime = startDate.AddHours(1),
                    TimeZone = timeZone
                },
                Recurrence = new List<string>()
                {
                    recurrence
                },
                Reminders = new Reminder
                {
                    UseDefault = false,
                    Overrides = new List<Override>()
                    {
                        new Override
                        {
                            Method = reminderMethod,
                            Minutes = reminderMinutes
                        }
                    }
                }
            };
            using (var content = new StringContent(JsonConvert.SerializeObject(newEvents), Encoding.UTF8, MediaTypeNames.Application.Json))
            {
                var uri = new System.Uri("https://www.googleapis.com/calendar/v3/calendars/" + $"{calendarId}/events");
                var response = this._httpClient.PostAsync(uri, content);
                return await response;
            }

           // return await this._httpClient.PostAsJsonAsync($"calendars/{calendarId}/events", newEvents);
        }
    }
}