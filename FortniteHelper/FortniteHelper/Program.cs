using FortniteHelper;
using FortniteHelper.Models;
using FortniteHelper.Clients;
using Newtonsoft.Json.Linq;
using Amazon.Runtime;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;



namespace FortniteHelper
{


    public class Program
    {
        
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.\
            var credentials = new BasicAWSCredentials(Constats.AccesKey, Constats.SecretAccesKey);
            var config = new AmazonDynamoDBConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.EUNorth1 // ¬каж≥ть ваш рег≥он
            };

            var client = new AmazonDynamoDBClient(credentials, config);
            builder.Services.AddSingleton<IAmazonDynamoDB>(client);
            builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            builder.Services.AddSingleton<IMyStats, MyStatsClient>();
            builder.Services.AddSingleton<MyStatsClient>();
            builder.Services.AddSingleton<IDeleteMyStats, DeleteMyStatsClient>();
            builder.Services.AddSingleton<DeleteMyStatsClient>();
            builder.Services.AddSingleton<IUpdateMyStats, UpdateMyStatsClient>();
            builder.Services.AddSingleton<UpdateMyStatsClient>();
            builder.Services.AddSingleton<IShowMyStats, ShowMyStatsClient>();
            builder.Services.AddSingleton<ShowMyStatsClient>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.MapControllers();

            app.Run();

        }
       


    }
}
