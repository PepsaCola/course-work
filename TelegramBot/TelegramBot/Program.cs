using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;
using Telegram.BotAPI.UpdatingMessages;
using Telegram.BotAPI.Stickers;
using TelegramBot.Models;
using static System.Net.WebRequestMethods;
using Fortnite_API;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace TelegramBot
{
    class Program
    {
        private static string _address = Constant.Address;
        private static string _apikey = Constant.ApiKey;
        private static string _token = Constant.Token;
       
        static async Task Main(string[] args)
        {
            var client = new TelegramBotClient(_token);
            
            
            var updates = client.GetUpdates();
            while (true)
            {

                if (updates.Any())
                {
                    foreach (var update in updates)
                    {

                        if (update.Message != null)
                        {
                           
                            var chatId = update.Message.Chat.Id;
                            string username = update.Message.Chat.Username;
                            Console.WriteLine("Chat Id: " + chatId);
                            Console.WriteLine("Username: " + username);

                            long unixTimestamp = update.Message.Date;
                            DateTime dateTime = DateTimeOffset
                                .FromUnixTimeSeconds(unixTimestamp)
                                .DateTime
                                .AddHours(3);
                            Console.WriteLine(update.Message.Text);
                            Console.WriteLine("Time: " + dateTime);
                            
                            string keyword = update.Message.Text;
                            if (keyword != null && keyword == "/start")
                                await Start(client, chatId);
                            if (keyword != null && keyword == "/getmap")
                                await GetCurrentMap(client, chatId);

                            if (keyword != null && keyword == "/getnews")
                                await GetNews(client, chatId);

                            if (keyword != null && keyword == "/getshop")
                                await GetShop(client, chatId);

                            if(keyword!=null&&keyword == "/keyboard")
                                await Keyboard(client, chatId);

                            if (keyword != null && keyword.StartsWith("/getstats"))
                            {
                                var parts = keyword.Split(' ');
                                if (parts.Length >= 2)
                                {
                                    string name = string.Join(' ', parts.Skip(1));
                                    await GetStats(client, chatId, name);
                                }
                                else
                                {
                                    await client.SendMessageAsync(
                                        chatId: chatId,
                                        text: "/getstats <name>"
                                    );
                                }
                            }

                            if (keyword != null && keyword.StartsWith("/mystats"))
                            {
                                var parts = keyword.Split(' ');
                                if (parts.Length >= 2)
                                {
                                    string name = string.Join(' ', parts.Skip(1));
                                    if (name == "update")
                                    {
                                        await PutMyStats(client, chatId);
                                    }
                                    else if (name == "remove") 
                                    {
                                        await DeleteMyStats(client, chatId);
                                    }
                                    else if(name == "show")
                                    {
                                        await ShowMyStats(client, chatId);
                                    }
                                    else
                                    {
                                        await PostMyStats(client, chatId, name);
                                    }
                                }
                                else
                                {
                                    await client.SendMessageAsync(
                                        chatId: chatId,
                                        text: "/mystats <name> \n/mystats update \n/mystats remove \n/mystats show"
                                    );
                                }
                            }
                            Console.WriteLine();
                        }
                    }
                    var offset = updates.Last().UpdateId + 1;
                    updates = client.GetUpdates(offset);
                }
                else
                {
                    updates = client.GetUpdates();
                }
            }
        }


        private static async Task GetShop(TelegramBotClient client, long chatId)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://localhost:7119/GetShop");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var shopData = JsonConvert.DeserializeObject<ShopModel>(json);
            try
            {
                if (shopData.featured.entries != null)
                {
                    foreach (var shop in shopData.featured.entries)
                    {
                        if (shop.bundle != null)
                        {
                            client.SendPhoto(
                                chatId = chatId,
                                photo: $"{shop.bundle.image}",
                                caption: $"{shop.bundle.name}" +
                                        $"\nRegular price: {shop.regularPrice} V-Bucks" +
                                        $"\nFinal price: {shop.finalPrice} V-Bucks"
                                );
                        }
                        else if (shop.newDisplayAsset != null)
                        {
                            var photoUrl = shop.newDisplayAsset.materialInstances[0].images["OfferImage"];
                            Console.WriteLine("Photo URL (New Display Asset): " + photoUrl);
                            client.SendPhoto(
                                chatId = chatId,
                                photo: $"{shop.newDisplayAsset.materialInstances[0].images["OfferImage"]}",
                                caption: $"{shop.items[0].name}" +
                                        $"\nRegular price: {shop.regularPrice} V-Bucks" +
                                        $"\nFinal price: {shop.finalPrice} V-Bucks");
                        }
                        else
                        {
                            client.SendPhoto(
                               chatId = chatId,
                               photo: $"{shop.items[0].images.icon}",
                               caption: $"{shop.items[0].name}" +
                                       $"\nRegular price: {shop.regularPrice} V-Bucks" +
                                       $"\nFinal price: {shop.finalPrice} V-Bucks");
                        };
                    }
                }
            }
            catch (Telegram.BotAPI.BotRequestException ex)
            {
                Console.WriteLine("Telegram API Error: " + ex.Message);
                // Handle specific errors here, if necessary
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                // Handle other errors here, if necessary
            }
        }


        private static async Task GetNews(TelegramBotClient client, long chatId)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://localhost:7119/GetNews");
            response.EnsureSuccessStatusCode();


            string json = await response.Content.ReadAsStringAsync();
            var mapData = JsonConvert.DeserializeObject<NewsModel>(json);

            if (mapData.br.image != null)
            {
                client.SendVideo(
                    chatId: chatId,
                    video: mapData.br.image
                    );
                foreach (var map in mapData.br.motds)
                {
                    client.SendPhoto(
                        chatId = chatId,
                        photo: map.image,
                        caption: $"{map.title}" +
                                 $"\n{map.body}"
                );                       
                }
            }
        }


        async static Task GetCurrentMap(TelegramBotClient client,long chatId )
        {

            HttpClient httpClient = new HttpClient();
           
            var response = await httpClient.GetAsync("https://localhost:7119/GetMap");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var mapData = JsonConvert.DeserializeObject<GetMap>(json);
            
            if (mapData.Images.Pois != null ) {
                client.SendPhoto(
                    chatId: chatId,
                    photo: mapData.Images.Pois
                    );
            }  
        }


        async static Task GetStats(TelegramBotClient client,long chatId, string keyword)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", _apikey);

            var response = await httpClient.GetAsync($"https://localhost:7119/GetStats?keyword={keyword}");

            if (!response.IsSuccessStatusCode)
                {
                    await client.SendMessageAsync(
                        chatId: chatId,
                        text: $"Error: {response.ReasonPhrase}"
                    );
                    return;
                }
                string json = await response.Content.ReadAsStringAsync();
                var statsData = JsonConvert.DeserializeObject<GetStats>(json);
            
            client.SendMessage(
                chatId: chatId,
                text: $"Name: {statsData.account.name}\n" +
                $"\nBattle Pass" +
                $"\nLevel: {statsData.battlePass.level}\n" +
                $"\nStats" +
                $"\nAll" +
                $"\nWins: {statsData.stats.all.overall.wins}" +
                $"\nTop 10: {statsData.stats.all.overall.top10}" +
                $"\nTop 25: {statsData .stats.all.overall.top25}" +
                $"\nKills: {statsData.stats.all.overall.kills}" +
                $"\nKills per match: {statsData.stats.all.overall.killsPerMatch}" +
                $"\nK/D: {statsData.stats.all.overall.kd}" +
                $"\nMatches: {statsData.stats.all.overall.matches}" +
                $"\nWin Rate: {statsData.stats.all.overall.winRate}%\n"
                
                
               
                );
            if (statsData.stats.all.solo!=null) {
                client.SendMessage(
                    chatId: chatId,
                    text:
                $"\nSolo" +
                $"\nWins: {statsData.stats.all.solo.wins}" +
                $"\nTop 10: {statsData.stats.all.solo.top10}" +
                $"\nTop 25: {statsData.stats.all.solo.top25}" +
                $"\nKills: {statsData.stats.all.solo.kills}" +
                $"\nKills per match: {statsData.stats.all.solo.killsPerMatch}" +
                $"\nK/D: {statsData.stats.all.solo.kd}" +
                $"\nMatches: {statsData.stats.all.solo.matches}" +
                $"\nWin Rate: {statsData.stats.all.solo.winRate}%\n"
                    );
            }
            if (statsData.stats.all.duo != null)
            {
                client.SendMessage(
                    chatId: chatId,
                    text: $"\nDuo" +
                $"\nWins: {statsData.stats.all.duo.wins}" +
                $"\nTop 10: {statsData.stats.all.duo.top10}" +
                $"\nTop 25: {statsData.stats.all.duo.top25}" +
                $"\nKills: {statsData.stats.all.duo.kills}" +
                $"\nKills per match: {statsData.stats.all.duo.killsPerMatch}" +
                $"\nK/D: {statsData.stats.all.duo.kd}" +
                $"\nMatches: {statsData.stats.all.duo.matches}" +
                $"\nWin Rate: {statsData.stats.all.duo.winRate}%\n");
            }
            if (statsData.stats.all.squad != null)
            {
                client.SendMessage(
                    chatId: chatId,
                    text: $"\nSquad" +
                $"\nWins: {statsData.stats.all.squad.wins}" +
                $"\nTop 10: {statsData.stats.all.squad.top10}" +
                $"\nTop 25: {statsData.stats.all.squad.top25}" +
                $"\nKills: {statsData.stats.all.squad.kills}" +
                $"\nKills per match: {statsData.stats.all.squad.killsPerMatch}" +
                $"\nK/D: {statsData.stats.all.squad.kd}" +
                $"\nMatches: {statsData.stats.all.squad.matches}" +
                $"\nWin Rate: {statsData.stats.all.squad.winRate}%");
            }
        }


        async static Task PostMyStats(TelegramBotClient client, long chatId, string name)
        {
            HttpClient httpClient = new HttpClient();
            var uri = $"https://localhost:7119/PostMyStats?chatId={chatId}&name={name}";
            var content = new StringContent($"", Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(uri,content);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                await client.SendMessageAsync(
                    chatId: chatId,
                    text: $"Error: {response.ReasonPhrase}"
                );
                return;
            }
            string json = await response.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<Message1>(json);

            await client.SendMessageAsync(
                 chatId: chatId,
                 text: answer.message);

            
        }


        async static Task PutMyStats(TelegramBotClient client, long chatId)
        {
            HttpClient httpClient = new HttpClient();
            var uri = $"https://localhost:7119/UpdateMyStats?chatId={chatId}";
            var content = new StringContent($"", Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(uri, content);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                await client.SendMessageAsync(
                    chatId: chatId,
                    text: $"Error: {response.ReasonPhrase}"
                );
                return;
            }
            string json = await response.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<Message1>(json);

            await client.SendMessageAsync(
                 chatId: chatId,
                 text: answer.message);
        }


        async static Task DeleteMyStats(TelegramBotClient client,long chatId)
        {
            HttpClient httpClient = new HttpClient();
            var uri = $"https://localhost:7119/DeleteMyStats?chatId={chatId}";
            var response = await httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<Message1>(json);
            await client.SendMessageAsync(
                 chatId: chatId,
                 text: answer.message);
           
            
        }

        async static Task ShowMyStats(TelegramBotClient client, long chatId)
        {
            HttpClient httpClient = new HttpClient();
            var uri = $"https://localhost:7119/ShowMyStats?chatId={chatId}";
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<Message1>(json);
            await client.SendMessageAsync(
                 chatId: chatId,
                 text: answer.message);
        }

        async static Task Keyboard(TelegramBotClient client, long chatId)
        {
            var keyboard = new KeyboardButton[][]
         {
            new KeyboardButton[] { new KeyboardButton("/getmap"), new KeyboardButton("/getnews") },
            new KeyboardButton[] { new KeyboardButton("/getstats"), new KeyboardButton("/getshop") },
            new KeyboardButton[] { new KeyboardButton("/mystats")}
         };

            var replyMarkup = new ReplyKeyboardMarkup(keyboard)
            {
                ResizeKeyboard = true
            };

            await client.SendMessageAsync(
                chatId: chatId,
                text: "Choose an option:",
                replyMarkup: replyMarkup
            );
        }
        async static Task Start(TelegramBotClient client, long chatId)
        {
            client.SendPhoto(
                               chatId = chatId,
                               photo:$"https://gaming-cdn.com/images/news/articles/6512/cover/here-s-a-little-preview-of-fallout-on-fortnite-cover664df1a5bea19.jpg",
                               caption: $"Hi, i am FortniteHelperBot" +
                               $"\nI was created to help fortnite players and make their life a little easier\n" +
                               $"\nMy possibilities:\n" +
                               $"\n-Get a current game map\n" +
                               $"\n-Receiving actual game news\n" +
                               $"\n-Receiving actual geme shop\n" +
                               $"\n-Getting statistics of the player specified by you\n" +
                               $"\n-Setting your account statistics\n" +
                               $"\n-Updating your account statistics\n" +
                               $"\n-Deleting your account statistics\n" +
                               $"\n-Getting your account statistics\n" +
                               $"\nSo, how can I help you?"
                               ) ;
        }
    }
}