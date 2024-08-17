using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace AzureServiceBusExample
{
    class AzureServiceBus
    {
        private const string connectionString = "<Your-Service-Bus-Connection-String>";
        private const string queueName = "<Your-Queue-Name>";

        static async Task Main(string[] args)
        {
            // Send a message
            await SendMessageAsync();

            // Receive a message
            await ReceiveMessageAsync();
        }

        private static async Task SendMessageAsync()
        {
            // Create a ServiceBusClient
            await using var client = new ServiceBusClient(connectionString);

            // Create a sender for the queue
            ServiceBusSender sender = client.CreateSender(queueName);

            // Create a message to send
            var message = new ServiceBusMessage("Hello, Service Bus!");

            // Send the message
            await sender.SendMessageAsync(message);

            Console.WriteLine("Message sent.");
        }

        private static async Task ReceiveMessageAsync()
        {
            // Create a ServiceBusClient
            await using var client = new ServiceBusClient(connectionString);

            // Create a receiver for the queue
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            // Receive a message
            ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

            if (receivedMessage != null)
            {
                Console.WriteLine($"Received message: {receivedMessage.Body}");
                // Complete the message so it is not received again
                await receiver.CompleteMessageAsync(receivedMessage);
            }
        }
    }
}
