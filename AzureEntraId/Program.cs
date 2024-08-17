using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace AzureEntraIDExample
{
    class Program
    {
        private const string clientId = "<Your-Client-ID>";
        private const string tenantId = "<Your-Tenant-ID>";
        private const string clientSecret = "<Your-Client-Secret>";
        private static readonly string[] scopes = { "https://graph.microsoft.com/.default" };

        static async Task Main(string[] args)
        {
            var app = ConfidentialClientApplicationBuilder.Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}"))
                .Build();

            var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            Console.WriteLine($"Access Token: {result.AccessToken}");
        }
    }
}
