using FortniteHelper.Clients;
using FortniteHelper.Models;
using Microsoft.AspNetCore.Mvc;

namespace FortniteHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowMyStatsController : Controller
    {
        private readonly ILogger<ShowMyStatsController> _logger;
        private readonly ShowMyStatsClient _myStatsClient;

        public ShowMyStatsController(ILogger<ShowMyStatsController> logger, ShowMyStatsClient myStatsClient)
        {
            _logger = logger;
            _myStatsClient = myStatsClient;
        }
        [HttpGet]
        public async Task<Message1> GetDataFromBD(long chatId)
        {

            try
            {
                Message1 mes1 = new Message1(await _myStatsClient.ShowDataFromBD(chatId));
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
