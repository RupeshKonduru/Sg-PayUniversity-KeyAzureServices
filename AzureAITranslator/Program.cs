using Azure;
using Azure.AI.Translation.Text;
using System;
using System.Threading.Tasks;

namespace RealTimeTranslation
{
    class Program
    {
        private const string endpoint = "<Your-Endpoint>";
        private const string apiKey = "<Your-Api-Key>";

        static async Task Main(string[] args)
        {
            var client = AuthenticateClient(endpoint, apiKey);
            var textToTranslate = "Hello, how are you?";

            var result = await TranslateTextAsync(client, textToTranslate, "en", "es");

            Console.WriteLine($"Original Text: {textToTranslate}");
            Console.WriteLine($"Translated Text: {result}");
        }

        static TextTranslationClient AuthenticateClient(string endpoint, string apiKey)
        {
            var client = new TextTranslationClient(new Uri(endpoint));
            return client;
        }

        static async Task<string> TranslateTextAsync(TextTranslationClient client, string text, string fromLanguage, string toLanguage)
        {
            var response = await client.TranslateAsync(text, fromLanguage, toLanguage);
            return response.Value.ToString();
        }
    }
}
