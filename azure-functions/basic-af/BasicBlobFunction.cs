using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Basic.Function;

public class BasicBlobFunction
{
    private readonly ILogger<BasicBlobFunction> _logger;

    public BasicBlobFunction(ILogger<BasicBlobFunction> logger)
    {
        _logger = logger;
    }

    // Basic example of blob trigger, input and output bindings
    // Next: http trigger with blob input, output binding
    [Function(nameof(BasicBlobFunction))]
    [BlobOutput("basicfa-blob-output/{name}-output.txt")]
    public async Task<string> Run(
        [BlobTrigger("basicfa-blob-trigger/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name,
        [BlobInput("basicfa-blob/HelloWorld.txt")] string inputContent)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        var newContent = content + '\n' + inputContent;
        
        _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");

        return newContent;
    }
}