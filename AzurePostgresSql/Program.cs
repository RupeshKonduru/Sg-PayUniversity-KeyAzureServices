using Npgsql;
using System;
using System.Threading.Tasks;

namespace AzurePostgresExample
{
    class Program
    {
        private const string connectionString = "Host=<your-server>.postgres.database.azure.com;Username=<your-username>;Password=<your-password>;Database=<your-database>";

        static async Task Main(string[] args)
        {
            using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT * FROM your_table;", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Console.WriteLine($"{reader.GetString(0)} - {reader.GetString(1)}");
            }
        }
    }
}
