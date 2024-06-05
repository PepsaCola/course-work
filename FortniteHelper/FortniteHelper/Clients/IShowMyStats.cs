namespace FortniteHelper.Clients
{
    public interface IShowMyStats
    {
        public Task<string> ShowDataFromBD(long chatId);
    }
}
