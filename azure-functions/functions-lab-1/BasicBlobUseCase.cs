using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Lab1.Function
{
    public class BasicBlobUseCase
    {
        private readonly ILogger<BasicBlobUseCase> _logger;

        public BasicBlobUseCase(ILogger<BasicBlobUseCase> logger)
        {
            _logger = logger;
        }

        [Function(nameof(BasicBlobUseCase))]
        [BlobOutput("usecase-output/{name}-processed.txt", Connection = "lab1usecase1jp_STORAGE")]
        public async Task<string> Run(
            [BlobTrigger("usecase1/{name}.txt", Connection = "lab1usecase1jp_STORAGE")] Stream stream, string name,
            [BlobInput("usecase-input/defaultMessage.txt", Connection = "lab1usecase1jp_STORAGE")] string inputBlobContent)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            content = $"{inputBlobContent} // {content}";
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
            return content;
        }
    }
}
