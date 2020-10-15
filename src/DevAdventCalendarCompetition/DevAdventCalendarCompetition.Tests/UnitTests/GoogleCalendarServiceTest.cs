using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services;
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
        public void CreateCalendarShouldReturnHttpresponseMessageTask()
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
                    var googleCalendarService = new GoogleCalendarService(httpClient, new AdventSettings(), new TestSettings(), new GoogleCalendarSettings());

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
    }
}
