using Azure;
using Azure.AI.Vision.ImageAnalysis;
using System;
using System.IO;

namespace SpeechRecognitionSample
{
    class AzureAIVision
    {
        static string endpoint = "https://sg-azure.cognitiveservices.azure.com/";
        static string key = "8b51aca8d19947faab481ee860d025d8";

        // Create an Image Analysis client.


        static async Task Main(string[] args)
        {
            ImageAnalysisClient client = new ImageAnalysisClient(new Uri(endpoint), new AzureKeyCredential(key));
            // Detect objects in the image.
            ImageAnalysisResult result = client.Analyze(
                new Uri("https://aka.ms/azsdk/image-analysis/sample.jpg"),
                VisualFeatures.Objects);

            // Print object detection results to the console
            Console.WriteLine($"Image analysis results:");
            Console.WriteLine($" Metadata: Model: {result.ModelVersion} Image dimensions: {result.Metadata.Width} x {result.Metadata.Height}");
            Console.WriteLine($" Objects:");
            foreach (DetectedObject detectedObject in result.Objects.Values)
            {
                Console.WriteLine($"   Object: '{detectedObject.Tags.First().Name}', Bounding box {detectedObject.BoundingBox.ToString()}");
            }
        }
    }
}
