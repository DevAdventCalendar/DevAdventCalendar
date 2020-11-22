using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DevAdventCalendarCompetition.Tests.UnitTests.GoogleCalendarServiceTests
{
    internal class TestMessageHandler : HttpMessageHandler
    {
        private readonly IDictionary<string, HttpResponseMessage> messages;

        public TestMessageHandler(IDictionary<string, HttpResponseMessage> messages)
        {
            this.messages = messages;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            if (this.messages.ContainsKey(request.RequestUri.ToString()))
            {
                response = this.messages[request.RequestUri.ToString()];
            }

            response.RequestMessage = request;
            return Task.FromResult(response);
        }
    }
}