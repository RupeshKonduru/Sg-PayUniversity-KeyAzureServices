using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace KafkaOnAzureExample
{
    class Program
    {
        private const string bootstrapServers = "<Your-Event-Hubs-Endpoint>";
        private const string topic = "<Your-Kafka-Topic>";

        static async Task Main(string[] args)
        {
            // Produce a message
            await ProduceMessageAsync();

            // Consume messages
            await ConsumeMessagesAsync();
        }

        private static async Task ProduceMessageAsync()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = "Hello, Kafka!" });
            Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
        }

        private static async Task ConsumeMessagesAsync()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = "test-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(topic);

            try
            {
                while (true)
                {
                    var cr = consumer.Consume();
                    Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error occurred: {e.Error.Reason}");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
