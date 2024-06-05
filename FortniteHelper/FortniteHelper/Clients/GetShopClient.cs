using FortniteHelper.Models;
using Newtonsoft.Json;

namespace FortniteHelper.Clients
{
    public class GetShopClient()
    {
        private static string _address;
        private static string _apikey;

        static GetShopClient()
        {
            _address = Constats.Address + "/v2/shop/br";
            _apikey = Constats.ApiKey;
        }
        public async Task<ShopModel> GetNews()
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
                var result = JsonConvert.DeserializeObject<ShopModel>(body);
                return result;


            }
        }
    }
}
