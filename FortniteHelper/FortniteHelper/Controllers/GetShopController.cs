using FortniteHelper.Clients;
using FortniteHelper.Models;
using Microsoft.AspNetCore.Mvc;

namespace FortniteHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetShopController : Controller
    {
        private readonly ILogger<GetShopController> _logger;


        public GetShopController(ILogger<GetShopController> logger)
        {
            _logger = logger;

        }
        [HttpGet]
        public DataShop GetNews()
        {
            GetShopClient client = new GetShopClient();
            ShopModel getNews = client.GetNews().Result;
            return getNews.data;

        }
    }
}
