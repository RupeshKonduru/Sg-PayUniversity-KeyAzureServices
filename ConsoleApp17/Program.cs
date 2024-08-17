using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;

namespace ApplicationInsightsExample
{
    class Program
    {
        private const string instrumentationKey = "<Your-Instrumentation-Key>";

        static void Main(string[] args)
        {
            var telemetryClient = new TelemetryClient { InstrumentationKey = instrumentationKey };

            // Track a custom event
            telemetryClient.TrackEvent("CustomEvent");

            // Track an exception
            try
            {
                throw new InvalidOperationException("Sample exception");
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
            }

            // Track some performance metrics
            var operation = telemetryClient.StartOperation<RequestTelemetry>("OperationName");
            try
            {
                // Your code here
            }
            finally
            {
                telemetryClient.StopOperation(operation);
            }

            Console.WriteLine("Telemetry sent.");
        }
    }
}
