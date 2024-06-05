using FortniteHelper.Clients;
using FortniteHelper.Models;
using Microsoft.AspNetCore.Mvc;

namespace FortniteHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetMapController : Controller
    {
        private readonly ILogger<GetMapController> _logger;
        

        public GetMapController(ILogger<GetMapController> logger)
        {
            _logger = logger;
            
        }
        [HttpGet]
        public Data GetMap1()
        {
            MapClient client = new MapClient();
            GetMap getMap = client.GetCurrentMap().Result;
            return getMap.Data;

        }
    }
}
