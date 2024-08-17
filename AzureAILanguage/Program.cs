using Azure;
using Azure.AI.TextAnalytics;
using System;
using System.Threading.Tasks;

namespace TextClassificationSample
{
    class Program
    {
        private const string endpoint = "<Your-Endpoint>";
        private const string apiKey = "<Your-Api-Key>";

        static async Task Main(string[] args)
        {
            var client = AuthenticateClient(endpoint, apiKey);
            var text = "Azure AI is a powerful suite of services for developers!";
            await DetectLanguageAsync(client, text);
            await AnalyzeSentimentAsync(client, text);
        }

        static TextAnalyticsClient AuthenticateClient(string endpoint, string apiKey)
        {
            var credentials = new AzureKeyCredential(apiKey);
            var client = new TextAnalyticsClient(new Uri(endpoint), credentials);
            return client;
        }

        static async Task DetectLanguageAsync(TextAnalyticsClient client, string text)
        {
            try
            {
                DetectedLanguage detectedLanguage = await client.DetectLanguageAsync(text);

                Console.WriteLine($"Text: {text}");
                Console.WriteLine($"Detected Language: {detectedLanguage.Name}");
                Console.WriteLine($"ISO6391 Name: {detectedLanguage.Iso6391Name}");
                Console.WriteLine($"Confidence Score: {detectedLanguage.ConfidenceScore}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        static async Task AnalyzeSentimentAsync(TextAnalyticsClient client, string text)
        {
            try
            {
                DocumentSentiment documentSentiment = await client.AnalyzeSentimentAsync(text);

                Console.WriteLine($"Text: {text}");
                Console.WriteLine($"Sentiment: {documentSentiment.Sentiment}");
                Console.WriteLine($"Positive Score: {documentSentiment.ConfidenceScores.Positive}");
                Console.WriteLine($"Neutral Score: {documentSentiment.ConfidenceScores.Neutral}");
                Console.WriteLine($"Negative Score: {documentSentiment.ConfidenceScores.Negative}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }


    }
}