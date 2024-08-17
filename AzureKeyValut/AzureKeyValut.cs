using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Threading.Tasks;

namespace AzureKeyVaultExample
{
    class AzureKeyValut
    {
        private const string keyVaultUrl = "<Your-Key-Vault-URL>";

        static async Task Main(string[] args)
        {
            // Initialize the SecretClient with DefaultAzureCredential
            var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

            // Retrieve a secret
            KeyVaultSecret secret = await client.GetSecretAsync("MySecretName");
            Console.WriteLine($"Secret value: {secret.Value}");
        }
    }
}
