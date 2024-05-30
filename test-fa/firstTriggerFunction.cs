using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace test_fa.Function
{
    public class firstTriggerFunction
    {
        private readonly ILogger<firstTriggerFunction> _logger;

        public firstTriggerFunction(ILogger<firstTriggerFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(firstTriggerFunction))]
        [BlobOutput("democontainer/{rand-guid}.txt", Connection = "AzureWebJobsStorage")]
        public ActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
