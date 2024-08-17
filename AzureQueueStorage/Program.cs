using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Threading.Tasks;

namespace AzureQueueStorageExample
{
    class Program
    {
        private const string connectionString = "<Your-Azure-Storage-Connection-String>";
        private const string queueName = "myqueue";

        static async Task Main(string[] args)
        {
            var queueClient = new QueueClient(connectionString, queueName);

            // Create the queue if it does not exist
            await queueClient.CreateIfNotExistsAsync();

            // Add a message to the queue
            Console.WriteLine("Adding message to the queue...");
            await queueClient.SendMessageAsync("Hello, Azure Queue Storage!");
            Console.WriteLine("Message added.");

            // Peek at the message in the queue
            Console.WriteLine("Peeking at the message...");
            PeekedMessage[] peekedMessages = (await queueClient.PeekMessagesAsync(1)).Value;
            foreach (var message in peekedMessages)
            {
                Console.WriteLine($"Peeked message: {message.MessageText}");
            }

            // Retrieve and delete the message
            Console.WriteLine("Retrieving and deleting the message...");
            QueueMessage[] retrievedMessages = (await queueClient.ReceiveMessagesAsync(1)).Value;
            foreach (var message in retrievedMessages)
            {
                Console.WriteLine($"Retrieved message: {message.MessageText}");
                await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
                Console.WriteLine("Message deleted.");
            }
        }
    }
}
