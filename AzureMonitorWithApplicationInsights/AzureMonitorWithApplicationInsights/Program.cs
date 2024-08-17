using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    private const string instrumentationKey = "<Your-Instrumentation-Key>";

    static async Task Main(string[] args)
    {
        // Initialize TelemetryClient
        var telemetryClient = new TelemetryClient { InstrumentationKey = instrumentationKey };

        // Track a custom event with properties as IDictionary<string, string>
        var properties = new Dictionary<string, string>
        {
            { "Property1", "Value1" }
        };
        telemetryClient.TrackEvent("CustomEvent", properties);

        // Track an exception
        try
        {
            throw new InvalidOperationException("Sample exception");
        }
        catch (Exception ex)
        {
            telemetryClient.TrackException(ex);
        }

        // Track a request
        var operation = telemetryClient.StartOperation<RequestTelemetry>("OperationName");
        try
        {
            // Simulate some work
            await Task.Delay(1000);
        }
        finally
        {
            telemetryClient.StopOperation(operation);
        }

        // Track a metric
        telemetryClient.TrackMetric("CustomMetric", 1.23);

        Console.WriteLine("Telemetry data sent.");
    }
}