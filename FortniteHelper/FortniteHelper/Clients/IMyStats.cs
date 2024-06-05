using FortniteHelper.Models;
using Telegram.Bot;

namespace FortniteHelper.Clients
{
    public interface IMyStats
    {
        public Task<string> PostDataToBD(long chatId, string name);
    }
}
