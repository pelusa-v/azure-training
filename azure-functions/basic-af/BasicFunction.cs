using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Basic.Function;

public class BasicFunction
{
    private readonly ILogger<BasicFunction> _logger;

    public BasicFunction(ILogger<BasicFunction> logger)
    {
        _logger = logger;
    }

    [Function("BasicFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}
