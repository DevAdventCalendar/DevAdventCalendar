using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Models;
using DevAdventCalendarCompetition.Services.Models.GoogleCalendar;
using DevAdventCalendarCompetition.Services.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.UnitTests
{
    public class GoogleCalendarServiceTest
    {
        [Fact]
        public void CreateCalendarShouldReturnHttpresponseMessageTaskAsync()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            using (var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            })
            {
                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(response);

                using (var httpClient = new HttpClient(handlerMock.Object))
                {
                    var googleCalendarService = new GoogleCalendarService(
                        httpClient,
                        new AdventSettings()
                        {
                            StartDate = new DateTime(2020, 12, 1),
                            EndDate = new DateTime(2020, 12, 24)
                        },
                        new TestSettings()
                        {
                            StartHour = new TimeSpan(12, 1, 0),
                            EndHour = new TimeSpan(12, 0, 0)
                        },
                        new GoogleCalendarSettings()
                        {
                            Summary = "Testowy opis"
                        });

                    // Act
                    var createdCalendar = googleCalendarService.CreateCalendar();

                    // Assert
                    Assert.NotNull(createdCalendar);
                    handlerMock
                        .Protected()
                        .Verify("SendAsync", Times.Exactly(0), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>());
                }
            }
        }

        [Fact]
        public void CreateCalendarWithEventsShouldReturnOperationResultTask()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            using (var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            })
            {
                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(response);

                using (var httpClient = new HttpClient(handlerMock.Object))
                {
                    var googleCalendarService = new GoogleCalendarService(
                        httpClient,
                        new AdventSettings()
                        {
                            StartDate = new DateTime(2020, 12, 1),
                            EndDate = new DateTime(2020, 12, 24)
                        },
                        new TestSettings()
                        {
                            StartHour = new TimeSpan(12, 1, 0),
                            EndHour = new TimeSpan(12, 0, 0)
                        },
                        new GoogleCalendarSettings()
                        {
                            Summary = "Testowy opis"
                        });

                    // Act
                    var createdCalendarWithEvents = googleCalendarService.CreateNewCalendarWithEvents();

                    // Assert
                    Assert.IsType<Task<OperationalResult>>(createdCalendarWithEvents);
                    handlerMock
                        .Protected()
                        .Verify("SendAsync", Times.Exactly(0), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>());
                }
            }
        }
    }
}
