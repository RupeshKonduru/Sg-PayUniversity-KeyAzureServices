using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace AzureRedisExample
{
    class AzureredisForCache
    {
        private const string redisConnectionString = "<your-redis-endpoint>:<your-port>,password=<your-password>,ssl=True,abortConnect=False";

        static async Task Main(string[] args)
        {
            var connection = ConnectionMultiplexer.Connect(redisConnectionString);
            var db = connection.GetDatabase();

            string key = "sample-key";
            string value = "Hello Redis";

            // Set value in Redis
            await db.StringSetAsync(key, value);
            Console.WriteLine("Value set in Redis.");

            // Get value from Redis
            var retrievedValue = await db.StringGetAsync(key);
            Console.WriteLine($"Value retrieved from Redis: {retrievedValue}");
        }
    }
}
