using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace TestDurableFunctions;

public static class SampleChain
{
    [Function(nameof(RunOrchestrator))]
    public static async Task<string> RunOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        ILogger logger = context.CreateReplaySafeLogger(nameof(RunOrchestrator));
        logger.LogInformation("Processing person");
        
        // Get input for the orchestrator
        var input = context.GetInput<string>();

        // Replace name and input with values relevant for your Durable Functions Activity
        var person = await context.CallActivityAsync<Person>(nameof(GetPerson), input);
        var isAdult = await context.CallActivityAsync<bool>(nameof(CheckAge), person);
        var message = await context.CallActivityAsync<string>(nameof(GenerateMessage), isAdult);

        return message;
    }

    [Function(nameof(GetPerson))]
    public static Person GetPerson([ActivityTrigger] string name, FunctionContext executionContext)
    {
        ILogger logger = executionContext.GetLogger("GetPerson");
        logger.LogInformation($"Name: {name}");

        return PersonBuilder.BuildPerson(name);
    }

    [Function(nameof(CheckAge))]
    public static bool CheckAge([ActivityTrigger] Person person, FunctionContext executionContext)
    {
        ILogger logger = executionContext.GetLogger("CheckAge");
        return person.Age > 18;
    }

    [Function(nameof(GenerateMessage))]
    public static string GenerateMessage([ActivityTrigger] bool isAdult, FunctionContext executionContext)
    {
        ILogger logger = executionContext.GetLogger("GenerateMessage");
        var message = isAdult ? "You are an adult." : "You are a minor.";
        logger.LogInformation(message);
        return message;
    }

    [Function(nameof(ExecuteOrchestratorByHttp))]
    public static async Task<HttpResponseData> ExecuteOrchestratorByHttp(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "people/{name}")] HttpRequestData req,
        string name,
        [DurableClient] DurableTaskClient client,
        FunctionContext executionContext)
    {
        ILogger logger = executionContext.GetLogger("ExecuteOrchestratorByHttp");

        // Function input comes from the request content.
        string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(RunOrchestrator), name);

        logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

        // Returns an HTTP 202 response with an instance management payload.
        // See https://learn.microsoft.com/azure/azure-functions/durable/durable-functions-http-api#start-orchestration
        return await client.CreateCheckStatusResponseAsync(req, instanceId);
    }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public static class PersonBuilder
{
    public static Person BuildPerson(string name)
    {
        Random random = new Random();
        return new Person
        {
            Name = name,
            Age = random.Next(1, 65)
        };
    }
}