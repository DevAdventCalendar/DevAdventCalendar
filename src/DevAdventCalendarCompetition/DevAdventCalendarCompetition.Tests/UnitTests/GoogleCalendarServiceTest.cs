using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
        public void CreateCalendarShouldReturnHttpResponseMessage()
        {
            // Arange
            var callendarMock = new Mock<NewCalendarDto>();
            var handlerMock = new Mock<HttpMessageHandler>();
            var adventSettingsMock = new Mock<AdventSettings>();
            var testSettingsMock = new Mock<TestSettings>();
            var calendarSettingsMock = new Mock<GoogleCalendarSettings>();

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);
            var calendarServiceMock = new Mock<GoogleCalendarService>();
            
            var retrievedPosts = await calendarServiceMock.CreateCallendar

            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post));

            // Act
            // Assert
        }
    }
}
