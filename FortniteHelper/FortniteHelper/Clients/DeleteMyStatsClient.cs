using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace FortniteHelper.Clients
{
    public class DeleteMyStatsClient: IDeleteMyStats
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly ILogger<DeleteMyStatsClient> _logger;

        public DeleteMyStatsClient(IAmazonDynamoDB dynamoDb, ILogger<DeleteMyStatsClient> logger)
        {
            _logger = logger;
            _dynamoDb = dynamoDb;
        }
        public async Task<string> DeleteDataFromBD(long chatId)
        {
           
            string tableName = "TelegramBot_FortniteHelper";
            var request_get = new GetItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } },
            };
            var response_get = await _dynamoDb.GetItemAsync(request_get);
            if (response_get.Item == null || !response_get.Item.ContainsKey("chatId"))
            {
                return "Nothing to delete. You can add your account.\n/mystats <name>";
            }
            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } },
            };
            await _dynamoDb.DeleteItemAsync(request);

            var request_get_solo = new GetItemRequest
            {
                TableName = "TelegramBot_FortniteHelper_solo",
                Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } },
            };
            if (_dynamoDb.GetItemAsync(request_get_solo) != null)
            {
                var request_solo = new DeleteItemRequest
                {
                    TableName = "TelegramBot_FortniteHelper_solo",
                    Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } },
                };
                await _dynamoDb.DeleteItemAsync(request_solo);
            }

            var request_get_duo = new GetItemRequest
            {
                TableName ="TelegramBot_FortniteHelper_duo",
                Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } },
            };
            if (_dynamoDb.GetItemAsync(request_get_duo) != null)
            {
                var request_duo = new DeleteItemRequest
                {
                    TableName ="TelegramBot_FortniteHelper_duo",
                    Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } },
                };
                await _dynamoDb.DeleteItemAsync(request_duo);
            }

            var request_get_squad = new GetItemRequest
            {
                TableName ="TelegramBot_FortniteHelper_squad",
                Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } },
            };
            if (_dynamoDb.GetItemAsync(request_get_squad) != null)
            {
                var request_squad = new DeleteItemRequest
                {
                    TableName ="TelegramBot_FortniteHelper_squad",
                    Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } },
                };
                await _dynamoDb.DeleteItemAsync(request_squad);
            }
            return "Your account was removed";

        }
    }
}
