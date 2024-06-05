using Amazon.DynamoDBv2.Model;
using FortniteHelper.Clients;
using FortniteHelper.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Telegram.Bot;

namespace FortniteHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostMyStatsController : ControllerBase
    {
        private readonly ILogger<PostMyStatsController> _logger;
        private readonly MyStatsClient _myStatsClient;
        
        public PostMyStatsController(ILogger<PostMyStatsController> logger,MyStatsClient myStatsClient)
        {
            _logger = logger;
            _myStatsClient = myStatsClient;
        }
        [HttpPost]
        public async Task<Message1> PostDataToBD( long chatId,string name)
        {
            
            try
            {
                Message1 mes1= new Message1(await _myStatsClient.PostDataToBD(chatId, name));
                return mes1;
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Error in PostDataToBD");
                Message1 mes1 = new Message1(ex.ToString());
                return mes1;
            }
        }
    }
}
