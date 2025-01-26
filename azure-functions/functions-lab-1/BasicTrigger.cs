using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Lab1.Function
{
    public class BasicTrigger
    {
        private readonly ILogger<BasicTrigger> _logger;

        public BasicTrigger(ILogger<BasicTrigger> logger)
        {
            _logger = logger;
        }

        [Function("BasicTrigger")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
