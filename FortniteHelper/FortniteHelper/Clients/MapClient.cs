using FortniteHelper.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
namespace FortniteHelper.Clients
{
    public class MapClient
    {
        private static string _address;
        private static string _apikey;
        private readonly HttpClient _client;


        public MapClient()
        {
            _address = Constats.Address + "/v1/map";
            _apikey = Constats.ApiKey;
        }

        public async Task<GetMap> GetCurrentMap()
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
                var result = JsonConvert.DeserializeObject<GetMap>(body);
                return result;


            }
        }
    }
}
