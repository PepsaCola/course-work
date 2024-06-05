using FortniteHelper.Clients;
using FortniteHelper.Models;
using Microsoft.AspNetCore.Mvc;

namespace FortniteHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetStatsController : Controller
    {
        private readonly ILogger<GetNewsController> _logger;


        public GetStatsController(ILogger<GetNewsController> logger)
        {
            _logger = logger;

        }
        [HttpGet]
        public DataStats GetStats1(string keyword)
        {
            GetStatsClient client = new GetStatsClient();
            GetStatsModel getNews = client.GetStats(keyword).Result;
            return getNews.data;

        }
    }
}
