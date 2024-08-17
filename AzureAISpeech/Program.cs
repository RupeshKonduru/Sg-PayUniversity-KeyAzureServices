using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Threading.Tasks;

namespace SpeechRecognitionSample
{
    class Program
    {
        private const string subscriptionKey = "<Your-Subscription-Key>";
        private const string serviceRegion = "<Your-Service-Region>"; // e.g., "eastus"

        static async Task Main(string[] args)
        {
            var speechConfig = SpeechConfig.FromSubscription(subscriptionKey, serviceRegion);
            var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

            Console.WriteLine("Say something...");

            var result = await speechRecognizer.RecognizeOnceAsync();

            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                Console.WriteLine($"Recognized: {result.Text}");
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                Console.WriteLine("No speech could be recognized.");
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                }
            }
        }
    }
}
