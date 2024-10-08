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
    public class TimeTrigger
    {
        private readonly ILogger _logger;

        public TimeTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TimeTrigger>();
        }

        [FunctionName("TimeTrigger")]
        public void Run([TimerTrigger("*/1 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            var message = $"C# Timer trigger function executed at: {DateTime.Now}";

            log.LogInformation("Timer Function Executed");
        }

    }
}
