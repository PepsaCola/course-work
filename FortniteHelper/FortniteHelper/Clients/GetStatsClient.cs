using FortniteHelper.Models;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace FortniteHelper.Clients
{
    public class GetStatsClient
    {
        private static string _address;
        private static string _apikey;
        private readonly HttpClient _client;


        public GetStatsClient()
        {
            _address = Constats.Address;
            _apikey = Constats.ApiKey;
        }

        public async Task<GetStatsModel> GetStats(string keyword)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address+$"/v2/stats/br/v2?name={keyword}"),
                Headers =
                {
                    {"Authorization", _apikey}

                }
            };
            
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetStatsModel>(body);
                return result;


            }
        }
    }
}
