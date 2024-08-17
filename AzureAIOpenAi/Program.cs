using Azure;
using Azure.AI.TextAnalytics;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIWithAzureAI
{
    class Program
    {
        private const string openAIKey = "<Your-OpenAI-API-Key>";
        private const string openAIEndpoint = "https://api.openai.com/v1/engines/davinci/completions";
        private const string azureTextAnalyticsEndpoint = "<Your-Azure-Text-Analytics-Endpoint>";
        private const string azureTextAnalyticsKey = "<Your-Azure-Text-Analytics-API-Key>";

        static async Task Main(string[] args)
        {
            var textAnalyticsClient = AuthenticateAzureClient();

            Console.WriteLine("Enter a prompt for the AI (type 'exit' to quit):");

            while (true)
            {
                var prompt = Console.ReadLine();
                if (prompt?.ToLower() == "exit") break;

                var aiResponse = await GetAIResponseAsync(prompt);
                Console.WriteLine("AI Response:");
                Console.WriteLine(aiResponse);
                Console.WriteLine();

                var sentiment = await AnalyzeSentimentAsync(textAnalyticsClient, aiResponse);
                var keyPhrases = await ExtractKeyPhrasesAsync(textAnalyticsClient, aiResponse);

                Console.WriteLine($"Sentiment: {sentiment}");
                Console.WriteLine("Key Phrases:");
                foreach (var phrase in keyPhrases)
                {
                    Console.WriteLine($"- {phrase}");
                }
                Console.WriteLine();
            }
        }

        static async Task<string> GetAIResponseAsync(string prompt)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAIKey}");

            var requestBody = new
            {
                prompt = prompt,
                max_tokens = 150
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(openAIEndpoint, content);
            var responseString = await response.Content.ReadAsStringAsync();

            dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);
            return jsonResponse.choices[0].text.ToString().Trim();
        }

        static async Task<string> AnalyzeSentimentAsync(TextAnalyticsClient client, string text)
        {
            var response = await client.AnalyzeSentimentAsync(text);
            return response.Value.Sentiment.ToString();
        }

        static async Task<string[]> ExtractKeyPhrasesAsync(TextAnalyticsClient client, string text)
        {
            var response = await client.ExtractKeyPhrasesAsync(text);
            return response.Value.ToArray();
        }

        static TextAnalyticsClient AuthenticateAzureClient()
        {
            var credentials = new AzureKeyCredential(azureTextAnalyticsKey);
            return new TextAnalyticsClient(new Uri(azureTextAnalyticsEndpoint), credentials);
        }
    }
}
