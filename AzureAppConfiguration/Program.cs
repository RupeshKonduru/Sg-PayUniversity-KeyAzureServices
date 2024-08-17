using Azure.Data.AppConfiguration;
using System;
using System.Threading.Tasks;

namespace AzureAppConfigurationExample
{
    class Program
    {
        private const string connectionString = "<Your-App-Config-Connection-String>";

        static async Task Main(string[] args)
        {
            // Initialize the ConfigurationClient with a connection string
            var client = new ConfigurationClient(connectionString);

            // Retrieve a configuration value
            var setting = await client.GetConfigurationSettingAsync("MySettingKey");
            Console.WriteLine($"Value of 'MySettingKey': {setting.Value.Value}");
        }
    }
}
