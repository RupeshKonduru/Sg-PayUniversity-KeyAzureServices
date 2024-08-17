using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Azure_Functions
{
    public class QueueTrigger
    {

        private readonly ILogger _logger;

        public QueueTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<QueueTrigger>();
        }

        [FunctionName("QueueTrigger")]
        public void Run([QueueTrigger("message-queue", Connection = "AzureWebJobsStorage")] string myQueueItem, ILogger log)
        {
            // Send an email
            // validate

            // alert someone
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
