using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DevAdventCalendarCompetition.Services
{
    public class EmailNotificationService : INotificationService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IOptionsMonitor<EmailNotificationOptions> _optionsAccessor;

        public EmailNotificationService(IHttpClientFactory clientFactory, IOptionsMonitor<EmailNotificationOptions> optionsAccessor)
        {
            this._clientFactory = clientFactory;
            this._optionsAccessor = optionsAccessor;
        }

        public async Task<bool> SetSubscriptionPreferenceAsync(string email, bool subscribe)
        {
            using (var request = new HttpRequestMessage())
            using (var client = this._clientFactory.CreateClient())
            {
                this.ConfigureRequest(request, email, subscribe);

                try
                {
                    var response = await client.SendAsync(request).ConfigureAwait(false);
                    return response.IsSuccessStatusCode;
                }
                catch (HttpRequestException)
                {
                    return false;
                }
            }
        }

        private void ConfigureRequest(HttpRequestMessage request, string email, bool subscribe)
        {
            request.Method = HttpMethod.Post;
            request.RequestUri = subscribe
                ? this._optionsAccessor.CurrentValue.SubscribeUrl
                : this._optionsAccessor.CurrentValue.UnsubscribeUrl;
            request.Content = new FormUrlEncodedContent(this.CreateRequestData(email, subscribe));
        }

        private IEnumerable<KeyValuePair<string, string>> CreateRequestData(string email, bool subscribe)
        {
            var data = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("list", this._optionsAccessor.CurrentValue.ListId),
                new KeyValuePair<string, string>("boolean", "true")
            };

            if (subscribe)
            {
                data.Add(new KeyValuePair<string, string>("api_key", this._optionsAccessor.CurrentValue.ApiKey));
            }

            return data;
        }
    }
}
