using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Newtonsoft.Json.Linq;

namespace DevAdventCalendarCompetition.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly HttpClient _httpClient;

        public GoogleCalendarService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        // For demo purpose
        public async Task<IEnumerable<Items>> GetAllCalendars()
        {
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            var result = await this._httpClient.GetAsync("users/me/calendarList");
#pragma warning restore CA2234 // Pass system uri objects instead of strings
            string responseBody = await result.Content.ReadAsStringAsync();
            JObject googleSearch = JObject.Parse(responseBody);

            IList<JToken> results = googleSearch["items"].Children().ToList();
            IList<Items> searchResults = new List<Items>();
            foreach (JToken resultd in results)
            {
                Items searchResult = resultd.ToObject<Items>();
                searchResults.Add(searchResult);
            }

            return searchResults;
        }
    }
}