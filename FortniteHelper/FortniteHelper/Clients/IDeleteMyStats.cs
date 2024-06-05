namespace FortniteHelper.Clients
{
    public interface IDeleteMyStats
    {
        public Task<string> DeleteDataFromBD(long chatId);
    }
}
