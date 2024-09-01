using System;
using Azure.Storage.Blobs;

namespace practice_integration_api.Application;

public class BlobIntegration
{
    private readonly string _azureStorageConnString;

    public BlobIntegration(IConfiguration configuration)
    {
        _azureStorageConnString = configuration["Azure:Storage:ConnectionString"];
    }

    public void CopyBlob(string sourceContainer, string sourceBlob, string destContainer, string destBlob)
    {
        var sourceBlobClient = new BlobClient(_azureStorageConnString, sourceContainer, sourceBlob);
        var destBlobClient = new BlobClient(_azureStorageConnString, destContainer, destBlob);
        destBlobClient.StartCopyFromUriAsync(sourceBlobClient.Uri);
    }
}
