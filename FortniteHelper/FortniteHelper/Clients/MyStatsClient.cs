using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using FortniteHelper.Models;
using System.Globalization;

using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace FortniteHelper.Clients
{
    public class MyStatsClient : IMyStats
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly ILogger<MyStatsClient> _logger;
       
        public MyStatsClient(IAmazonDynamoDB dynamoDb, ILogger<MyStatsClient> logger)
        {
            _logger = logger;
            _dynamoDb = dynamoDb;
        }
        public async Task<string> PostDataToBD(long chatId, string name)
        {
            var tableName = "TelegramBot_FortniteHelper";
            var request_get = new GetItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "chatId", new AttributeValue { N = chatId.ToString() } } },
            };
            var response_get = await _dynamoDb.GetItemAsync(request_get);
            if (response_get.Item != null && response_get.Item.ContainsKey("chatId"))
            {
                return "You already have an account. If you want to install another one, uninstall this one first.\n/mystats <remove>";
            }
            else
            {
                var client = new GetStatsClient();
                GetStatsModel getStats = client.GetStats(name).Result;
                if (getStats.data.stats.all.overall != null )
                {
                    var request = new PutItemRequest
                    {
                        TableName = tableName,
                        Item = new Dictionary<string, AttributeValue>
                    {
                        { "chatId", new AttributeValue { N = chatId.ToString(CultureInfo.InvariantCulture) } },
                        { "name", new AttributeValue{S = name } },
                        { "wins", new AttributeValue{N= getStats.data.stats.all.overall.wins.ToString(CultureInfo.InvariantCulture)} },
                        { "kd", new AttributeValue{N = getStats.data.stats.all.overall.kd.ToString(CultureInfo.InvariantCulture) } },
                        { "kills", new AttributeValue{N = getStats.data.stats.all.overall.kills.ToString(CultureInfo.InvariantCulture)} },
                        { "killsPerMatch", new AttributeValue{N = getStats.data.stats.all.overall.killsPerMatch.ToString(CultureInfo.InvariantCulture) } },
                        { "matches", new AttributeValue{N = getStats.data.stats.all.overall.matches.ToString(CultureInfo.InvariantCulture)} },
                        { "top10", new AttributeValue{N = getStats.data.stats.all.overall.top10.ToString(CultureInfo.InvariantCulture) } },
                        { "top25", new AttributeValue{N = getStats.data.stats.all.overall.top25.ToString(CultureInfo.InvariantCulture) } },
                        { "winRate", new AttributeValue{N = getStats.data.stats.all.overall.winRate.ToString(CultureInfo.InvariantCulture) } }
                    }
                    };
                    try
                    {
                        await _dynamoDb.PutItemAsync(request);
                        Console.WriteLine("Item added successfully.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error adding item: {e.Message}");

                    }
                }

                if (getStats.data.stats.all.solo != null)
                {
                    var request_solo = new PutItemRequest
                    {
                        TableName = "TelegramBot_FortniteHelper_solo",
                        Item = new Dictionary<string, AttributeValue>
                    {
                        { "chatId", new AttributeValue { N = chatId.ToString(CultureInfo.InvariantCulture) } },
                        { "name", new AttributeValue{S = name } },
                        { "wins", new AttributeValue{N= getStats.data.stats.all.solo.wins.ToString(CultureInfo.InvariantCulture)} },
                        { "kd", new AttributeValue{N = getStats.data.stats.all.solo.kd.ToString(CultureInfo.InvariantCulture) } },
                        { "kills", new AttributeValue{N = getStats.data.stats.all.solo.kills.ToString(CultureInfo.InvariantCulture)} },
                        { "killsPerMatch", new AttributeValue{N = getStats.data.stats.all.solo.killsPerMatch.ToString(CultureInfo.InvariantCulture) } },
                        { "matches", new AttributeValue{N = getStats.data.stats.all.solo.matches.ToString(CultureInfo.InvariantCulture)} },
                        { "top10", new AttributeValue{N = getStats.data.stats.all.solo.top10.ToString(CultureInfo.InvariantCulture) } },
                        { "top25", new AttributeValue{N = getStats.data.stats.all.solo.top25.ToString(CultureInfo.InvariantCulture) } },
                        { "winRate", new AttributeValue{N = getStats.data.stats.all.solo.winRate.ToString(CultureInfo.InvariantCulture) } }
                    }
                    };
                    try
                    {
                        await _dynamoDb.PutItemAsync(request_solo);
                        Console.WriteLine("Item added successfully.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error adding item: {e.Message}");
                    }
                }

                if (getStats.data.stats.all.duo != null)
                {
                    var request_duo = new PutItemRequest
                    {
                        TableName = "TelegramBot_FortniteHelper_duo",
                        Item = new Dictionary<string, AttributeValue>
                    {
                        { "chatId", new AttributeValue { N = chatId.ToString(CultureInfo.InvariantCulture) } },
                        { "name", new AttributeValue{S = name } },
                        { "wins", new AttributeValue{N= getStats.data.stats.all.duo.wins.ToString(CultureInfo.InvariantCulture)} },
                        { "kd", new AttributeValue{N = getStats.data.stats.all.duo.kd.ToString(CultureInfo.InvariantCulture) } },
                        { "kills", new AttributeValue{N = getStats.data.stats.all.duo.kills.ToString(CultureInfo.InvariantCulture)} },
                        { "killsPerMatch", new AttributeValue{N = getStats.data.stats.all.duo.killsPerMatch.ToString(CultureInfo.InvariantCulture) } },
                        { "matches", new AttributeValue{N = getStats.data.stats.all.duo.matches.ToString(CultureInfo.InvariantCulture)} },
                        { "top10", new AttributeValue{N = getStats.data.stats.all.duo.top10.ToString(CultureInfo.InvariantCulture) } },
                        { "top25", new AttributeValue{N = getStats.data.stats.all.duo.top25.ToString(CultureInfo.InvariantCulture) } },
                        { "winRate", new AttributeValue{N = getStats.data.stats.all.duo.winRate.ToString(CultureInfo.InvariantCulture) } }
                    }
                    };
                    try
                    {
                        await _dynamoDb.PutItemAsync(request_duo);
                        Console.WriteLine("Item added successfully.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error adding item: {e.Message}");
                    }
                }
                if (getStats.data.stats.all.squad != null)
                {
                    var request_squad = new PutItemRequest
                    {
                        TableName = "TelegramBot_FortniteHelper_squad",
                        Item = new Dictionary<string, AttributeValue>
                    {
                        { "chatId", new AttributeValue { N = chatId.ToString(CultureInfo.InvariantCulture) } },
                        { "name", new AttributeValue{S = name } },
                        { "wins", new AttributeValue{N= getStats.data.stats.all.squad.wins.ToString(CultureInfo.InvariantCulture)} },
                        { "kd", new AttributeValue{N = getStats.data.stats.all.squad.kd.ToString(CultureInfo.InvariantCulture) } },
                        { "kills", new AttributeValue{N = getStats.data.stats.all.squad.kills.ToString(CultureInfo.InvariantCulture)} },
                        { "killsPerMatch", new AttributeValue{N = getStats.data.stats.all.squad.killsPerMatch.ToString(CultureInfo.InvariantCulture) } },
                        { "matches", new AttributeValue{N = getStats.data.stats.all.squad.matches.ToString(CultureInfo.InvariantCulture)} },
                        { "top10", new AttributeValue{N = getStats.data.stats.all.squad.top10.ToString(CultureInfo.InvariantCulture) } },
                        { "top25", new AttributeValue{N = getStats.data.stats.all.squad.top25.ToString(CultureInfo.InvariantCulture) } },
                        { "winRate", new AttributeValue{N = getStats.data.stats.all.squad.winRate.ToString(CultureInfo.InvariantCulture) } }
                    }
                    };
                    try
                    {
                        await _dynamoDb.PutItemAsync(request_squad);
                        Console.WriteLine("Item added successfully.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error adding item: {e.Message}");
                    }
                }
                return "Your stats were sets.";
            }
        }
    }
}
