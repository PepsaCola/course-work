namespace FortniteHelper.Clients
{
    public interface IUpdateMyStats
    {
        public Task<string> PutDataInBD(long chatId);
    }
}

