using FortniteHelper.Clients;
using FortniteHelper.Models;
using Microsoft.AspNetCore.Mvc;

namespace FortniteHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteMyStatsController : Controller
    {
        private readonly ILogger<DeleteMyStatsController> _logger;
        private readonly DeleteMyStatsClient _myStatsClient;

        public DeleteMyStatsController(ILogger<DeleteMyStatsController> logger, DeleteMyStatsClient myStatsClient)
        {
            _logger = logger;
            _myStatsClient = myStatsClient;
        }
        [HttpDelete]
        public async Task<Message1> DeleteDataFromBD(long chatId)
        {

            try
            {
                Message1 mes1 = new Message1(await _myStatsClient.DeleteDataFromBD(chatId));
                return mes1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PostDataToBD");
                Message1 mes1 = new Message1("Bad Request");
                return mes1;
            }
        }
    }
}
