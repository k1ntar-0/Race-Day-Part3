using System.Net.Http.Json;
using System.Text.Json;

namespace RaceDay.Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("RaceDayApi");
        }

        public async Task<List<JsonElement>?> GetEventsAsync()
        {
            return await _client.GetFromJsonAsync<List<JsonElement>>("api/Events");
        }

        // Add additional methods for Login, Register, Categories, Enrolments, etc.
    }
}