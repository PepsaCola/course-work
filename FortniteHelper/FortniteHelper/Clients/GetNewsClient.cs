using FortniteHelper.Models;
using Newtonsoft.Json;
using System.Net;

namespace FortniteHelper.Clients
{

    public class GetNewsClient()
    {
        private static string _address;
        private static string _apikey;

        static GetNewsClient()
        {
            _address = Constats.Address + "/v2/news";
            _apikey = Constats.ApiKey;
        }
        public async Task<NewsModel> GetNews()
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address),
                Headers =
                {
                    {"X-RapidAPI-Key", _apikey}

                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<NewsModel>(body);
                return result;


            }
        }
    }
}
