using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Extensions;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Models.GoogleCalendar;
using DevAdventCalendarCompetition.Services.Options;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    /*
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            throw new NotImplementedException("Now we can setup this method with our mocking framework");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(this.Send(request));
        }
    }
    */

    public class GoogleCalendarServiceTest
    {
        private readonly GoogleCalendarSettings _googleCalendarSettings = new GoogleCalendarSettings()
        {
            Summary = "Testowy Opis",
            Events = new CalendarEvent()
            {
                Location = "Warsaw",
                ReminderMethod = "null",
                ReminderMinutes = 11,
                Summary = "Testowy event",
                TimeZone = "Warsaw"
            }
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

        private readonly HttpResponseMessage[] calendarResponses = new HttpResponseMessage[]
        {
            new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new CalendarDto() { Id = "1" }), Encoding.UTF8, MediaTypeNames.Application.Json)
            },
            new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest }
        };

        private readonly HttpResponseMessage[] eventResponses = new HttpResponseMessage[]
        {
            new HttpResponseMessage { StatusCode = HttpStatusCode.OK },
            new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest }
        };

        [Fact]
        public void CreateCalendarWithEventsShouldReturnOperationalResultCalendarFailureTask()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(this.calendarResponses[1]);

            using (var httpClient = new HttpClient(handlerMock.Object))
            {
                var googleCalendarService = new GoogleCalendarService(
                        httpClient,
                        this._adventSettings,
                        this._testSettings,
                        this._googleCalendarSettings);

                // Act
                var createdCalendar = googleCalendarService.CreateNewCalendarWithEvents();

                // Assert
                Assert.IsType<Task<OperationalResult>>(createdCalendar);
                Assert.Equal<OperationalResultStatus>(OperationalResultStatus.CalendarFailure, createdCalendar.Result.Status);
                handlerMock.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>());
            }
        }

        [Fact]
        public void CreateCalendarWithEventsShouldReturnOperationalResultEventFailureTask()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .SetupSequence<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(this.calendarResponses[0])
                .ReturnsAsync(this.eventResponses[1]);

            using (var httpClient = new HttpClient(handlerMock.Object))
            {
                var googleCalendarService = new GoogleCalendarService(
                            httpClient,
                            this._adventSettings,
                            this._testSettings,
                            this._googleCalendarSettings);

                // Act
                var createdCalendar = googleCalendarService.CreateNewCalendarWithEvents();

                // Assert
                Assert.IsType<Task<OperationalResult>>(createdCalendar);
                Assert.Equal<OperationalResultStatus>(OperationalResultStatus.EventsFailure, createdCalendar.Result.Status);

                handlerMock.Protected().Verify("SendAsync", Times.Exactly(2), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>());
            }
        }

        [Fact]
        public void CreateCalendarWithEventsShouldReturnOperationalResultSuccesTask()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .SetupSequence<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(this.calendarResponses[0])
                .ReturnsAsync(this.eventResponses[0]);

            using (var httpClient = new HttpClient(handlerMock.Object))
            {
                var googleCalendarService = new GoogleCalendarService(
                            httpClient,
                            this._adventSettings,
                            this._testSettings,
                            this._googleCalendarSettings);

                // Act
                var createdCalendar = googleCalendarService.CreateNewCalendarWithEvents();

                // Assert
                Assert.IsType<Task<OperationalResult>>(createdCalendar);
                Assert.Equal<OperationalResultStatus>(OperationalResultStatus.Success, createdCalendar.Result.Status);

                handlerMock.Protected().Verify("SendAsync", Times.Exactly(2), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>());
            }
        }
    }
}
