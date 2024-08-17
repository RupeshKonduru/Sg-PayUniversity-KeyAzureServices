using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace OrderProcessingApp
{
    class AzureMonitorWithApplicationInsights
    {
        static void Main(string[] args)
        {
            // Set up Application Insights TelemetryClient
            var telemetryConfig = TelemetryConfiguration.CreateDefault();
            telemetryConfig.ConnectionString = "InstrumentationKey=your-instrumentation-key;";
            var telemetryClient = new TelemetryClient(telemetryConfig);

            // Simulate processing orders
            ProcessOrders(telemetryClient);

            // Flush telemetry before the application exits
            telemetryClient.Flush();
            System.Threading.Thread.Sleep(1000);  // Allow time for flushing
        }

        static void ProcessOrders(TelemetryClient telemetryClient)
        {
            var orders = new[]
            {
                new Order { Id = 1, Amount = 100 },
                new Order { Id = 2, Amount = 200 },
                new Order { Id = 3, Amount = 0 }  // This will cause an exception
            };

            foreach (var order in orders)
            {
                try
                {
                    // Simulate order processing
                    if (order.Amount <= 0)
                    {
                        throw new ArgumentException($"Invalid order amount for Order ID: {order.Id}.");
                    }

                    // Track custom event for successful order processing
                    telemetryClient.TrackEvent("OrderProcessed", new System.Collections.Generic.Dictionary<string, string>
                    {
                        { "OrderId", order.Id.ToString() },
                        { "Amount", order.Amount.ToString() }
                    });

                    Console.WriteLine($"Order {order.Id} processed successfully!");
                }
                catch (Exception ex)
                {
                    // Track exceptions
                    telemetryClient.TrackException(ex);

                    Console.WriteLine($"Error processing Order {order.Id}: {ex.Message}");
                }
            }
        }

        class Order
        {
            public int Id { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
