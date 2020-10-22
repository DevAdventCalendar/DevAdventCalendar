using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Models.GoogleCalendar;
using DevAdventCalendarCompetition.Services.Options;
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
            Summary = "Testowy Opis"
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
        public void CreateCalendarWithEventsShouldReturnOperationalResultTask()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>() { CallBase = true };
            using (var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest
            })
            {
                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(response);

                using (var httpClient = new HttpClient(handlerMock.Object))
                {
                    var newCalendar = new NewCalendarDto
                    {
                        Summary = this._googleCalendarSettings.Summary
                    };

                    using (var content = new StringContent(JsonConvert.SerializeObject(newCalendar), Encoding.UTF8, MediaTypeNames.Application.Json))
                    {
                        var uri = new Uri("https://calendar.google.com/calendars/");
                        httpClient.PostAsync(uri, content);

                        var googleCalendarService = new GoogleCalendarService(
                            httpClient,
                            this._adventSettings,
                            this._testSettings,
                            this._googleCalendarSettings);

                        // Act
                        var createdCalendar = googleCalendarService.CreateNewCalendarWithEvents();

                        // Assert
                        Assert.IsType<Task<OperationalResult>>(createdCalendar);
                        Assert.Same(OperationalResult.Failure(OperationalResultStatus.CalendarFailure), createdCalendar);

                        // handlerMock.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>());
                    }
                }
            }
        }
    }
}
