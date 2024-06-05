using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace FortniteHelper.Clients
{
    public class ShowMyStatsClient:IShowMyStats
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly ILogger<ShowMyStatsClient> _logger;

        public ShowMyStatsClient(IAmazonDynamoDB dynamoDb, ILogger<ShowMyStatsClient> logger)
        {
            _logger = logger;
            _dynamoDb = dynamoDb;
        }
        public async Task<string> ShowDataFromBD(long chatId)
        {
            var tableName = "TelegramBot_FortniteHelper";
            var request_get_overall = new GetItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } }
            };
            var response_get_overall = await _dynamoDb.GetItemAsync(request_get_overall);

            if (response_get_overall.Item == null || !response_get_overall.Item.ContainsKey("chatId"))
            {
                return "You don't set your account. If you want, write on chat \n/mystats <name>";
            }

            var request_get_solo = new GetItemRequest
            {
                TableName = tableName+"_solo",
                Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } }
            };
            var response_get_solo = await _dynamoDb.GetItemAsync(request_get_solo);

            var request_get_duo = new GetItemRequest
            {
                TableName = tableName+"_duo",
                Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } }
            };
            var response_get_duo = await _dynamoDb.GetItemAsync(request_get_duo);

            var request_get_squad = new GetItemRequest
            {
                TableName = tableName+"_squad",
                Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } }
            };
            var response_get_squad = await _dynamoDb.GetItemAsync(request_get_squad);
            var stats = $"Name: {response_get_overall.Item["name"].S}\n" +
                $"\nStats" +
                $"\nAll" +
                $"\nWins: {response_get_overall.Item["wins"].N}" +
                $"\nTop 10: {response_get_overall.Item["top10"].N}" +
                $"\nTop 25: {response_get_overall.Item["top25"].N}" +
                $"\nKills: {response_get_overall.Item["kills"].N}" +
                $"\nKills per match: {response_get_overall.Item["killsPerMatch"].N}" +
                $"\nK/D: {response_get_overall.Item["kd"].N}" +
                $"\nMatches: {response_get_overall.Item["matches"].N}" +
                $"\nWin Rate: {response_get_overall.Item["winRate"].N}%\n";
            if (response_get_solo.Item!=null && response_get_solo.Item.ContainsKey("chatId")) 
            {
                stats += $"\nSolo" +
                    $"\nWins: {response_get_solo.Item["wins"].N}" +
                    $"\nTop 10: {response_get_solo.Item["top10"].N}" +
                    $"\nTop 25: {response_get_solo.Item["top25"].N}" +
                    $"\nKills: {response_get_solo.Item["kills"].N}" +
                    $"\nKills per match: {response_get_solo.Item["killsPerMatch"].N}" +
                    $"\nK/D: {response_get_solo.Item["kd"].N}" +
                    $"\nMatches: {response_get_solo.Item["matches"].N}" +
                    $"\nWin Rate: {response_get_solo.Item["winRate"].N}%\n"; 
            }
            if (response_get_duo.Item!= null && response_get_duo.Item.ContainsKey("chatId"))
            {
                stats +=
                $"\nDuo" +
                $"\nWins: {response_get_duo.Item["wins"].N}" +
                $"\nTop 10: {response_get_duo.Item["top10"].N}" +
                $"\nTop 25: {response_get_duo.Item["top25"].N}" +
                $"\nKills: {response_get_duo.Item["kills"].N}" +
                $"\nKills per match: {response_get_duo.Item["killsPerMatch"].N}" +
                $"\nK/D: {response_get_duo.Item["kd"].N}" +
                $"\nMatches: {response_get_duo.Item["matches"].N}" +
                $"\nWin Rate: {response_get_duo.Item["winRate"].N}%\n";
            }
            if (response_get_squad.Item != null && response_get_squad.Item.ContainsKey("chatId"))
            {
                stats +=
                $"\nSquad" +
                $"\nWins: {response_get_squad.Item["wins"].N}" +
                $"\nTop 10: {response_get_squad.Item["top10"].N}" +
                $"\nTop 25: {response_get_squad.Item["top25"].N}" +
                $"\nKills: {response_get_squad.Item["kills"].N}" +
                $"\nKills per match: {response_get_squad.Item["killsPerMatch"].N}" +
                $"\nK/D: {response_get_squad.Item["kd"].N}" +
                $"\nMatches: {response_get_squad.Item["matches"].N}" +
                $"\nWin Rate: {response_get_squad.Item["winRate"].N}%";
            }
            return stats;
        }
    }
}
