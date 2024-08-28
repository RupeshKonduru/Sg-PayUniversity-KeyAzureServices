using Azure;
using Azure.AI.Translation.Text;
using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTranslation
{
    class AzureAITranslator
    {
        private static readonly string key = "8b51aca8d19947faab481ee860d025d8";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";
        private static readonly string fromLang = "en";
        private static readonly string toLang = "fr";
        // location, also known as region.
        // required if you're using a multi-service or regional (not global) resource. It can be found in the Azure portal on the Keys and Endpoint page.
        private static readonly string location = "eastus";

        static async Task Main(string[] args)
        {
            // Input and output languages are defined as parameters.
            string route = $"/translate?api-version=3.0&from={fromLang}&to={toLang}";
            string textToTranslate = "I would really like to drive your car around the block a few times!";
            Console.WriteLine(textToTranslate);
            object[] body = new object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                // location required if you're using a multi-service or regional (not global) resource.
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                Console.ReadKey();
            }
        }
    }
}
