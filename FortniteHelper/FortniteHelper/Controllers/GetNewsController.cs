using FortniteHelper.Clients;
using FortniteHelper.Models;
using Microsoft.AspNetCore.Mvc;

namespace FortniteHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetNewsController : Controller
    {
        private readonly ILogger<GetNewsController> _logger;


        public GetNewsController(ILogger<GetNewsController> logger)
        {
            _logger = logger;

        }
        [HttpGet]
        public DataNews GetNews()
        {
            GetNewsClient client = new GetNewsClient();
            NewsModel getNews = client.GetNews().Result;
            return getNews.data;

        }
    }
}
