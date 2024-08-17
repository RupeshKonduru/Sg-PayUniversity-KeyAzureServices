using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs; // Needed for blob storage
using System;
using System.Threading.Tasks;

namespace AzureEventHubsExample
{
    class Program
    {
        private const string connectionString = "<Your-Event-Hub-Connection-String>";
        private const string eventHubName = "<Your-Event-Hub-Name>";
        private const string consumerGroup = "$Default"; // Default consumer group
        private const string storageConnectionString = "<Your-Storage-Connection-String>";
        private const string blobContainerName = "<Your-Blob-Container-Name>";

        static async Task Main(string[] args)
        {
            // Send events
            await SendEventsAsync();

            // Receive events
            await ReceiveEventsAsync();
        }

        private static async Task SendEventsAsync()
        {
            await using var producerClient = new EventHubProducerClient(connectionString, eventHubName);

            using var batch = await producerClient.CreateBatchAsync();

            batch.TryAdd(new EventData("Event 1"));
            batch.TryAdd(new EventData("Event 2"));

            await producerClient.SendAsync(batch);

            Console.WriteLine("Events sent.");
        }

        private static async Task ReceiveEventsAsync()
        {
            var blobContainerClient = new BlobContainerClient(storageConnectionString, blobContainerName);

            // Ensure the blob container exists
            await blobContainerClient.CreateIfNotExistsAsync();

            var processorClient = new EventProcessorClient(
                blobContainerClient,
                consumerGroup,
                connectionString,
                eventHubName
            );

            processorClient.ProcessEventAsync += async (ProcessEventArgs eventArgs) =>
            {
                Console.WriteLine($"Event received: {eventArgs.Data.EventBody}");
                await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
            };

            processorClient.ProcessErrorAsync += async (ProcessErrorEventArgs errorArgs) =>
            {
                Console.WriteLine($"Error: {errorArgs.Exception.Message}");
            };

            await processorClient.StartProcessingAsync();

            Console.WriteLine("Receiving events. Press any key to exit.");
            Console.ReadKey();

            await processorClient.StopProcessingAsync();
        }
    }
}
