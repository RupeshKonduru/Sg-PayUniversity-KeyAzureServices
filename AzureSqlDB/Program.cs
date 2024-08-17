using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AzureSqlExample
{
    class Program
    {
        private const string connectionString = "Server=tcp:<your-server>.database.windows.net,1433;Database=<your-database>;User ID=<your-username>;Password=<your-password>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        static async Task Main(string[] args)
        {
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            var query = "SELECT * FROM your_table;";
            using var cmd = new SqlCommand(query, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Console.WriteLine($"{reader.GetString(0)} - {reader.GetString(1)}");
            }
        }
    }
}
