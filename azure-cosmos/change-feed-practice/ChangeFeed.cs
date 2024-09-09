using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Change.Feed.Practice
{
    public class ChangeFeed
    {
        private readonly ILogger _logger;

        public ChangeFeed(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ChangeFeed>();
        }

        [Function("ChangeFeed")]
        public void Run([CosmosDBTrigger(
            databaseName: "crud_practice1",
            containerName: "documents",
            Connection = "practicecosmos1_DOCUMENTDB",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Document> input)
        {
            if (input != null && input.Count > 0)
            {
                _logger.LogInformation("Documents modified: " + input.Count);
                _logger.LogInformation("First document Id: " + input[0].Id);
            }
        }
    }

    public class Document
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonProperty("available")]
        public bool Available { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
    }
}
