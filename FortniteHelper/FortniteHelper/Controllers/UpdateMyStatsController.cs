using FortniteHelper.Clients;
using FortniteHelper.Models;
using Microsoft.AspNetCore.Mvc;

namespace FortniteHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateMyStatsController : Controller
    {
        private readonly ILogger<UpdateMyStatsController> _logger;
        private readonly UpdateMyStatsClient _myStatsClient;

        public UpdateMyStatsController(ILogger<UpdateMyStatsController> logger, UpdateMyStatsClient myStatsClient)
        {
            _logger = logger;
            _myStatsClient = myStatsClient;
        }
        [HttpPut]
        public async Task<Message1> PostDataToBD(long chatId)
        {

            try
            {
                Message1 mes1 = new Message1(await _myStatsClient.PutDataInBD(chatId));
                return mes1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PostDataToBD");
                Message1 mes1 = new Message1(ex.ToString());
                return mes1;
            }
        }
    }
}
