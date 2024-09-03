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

    public void CopyBlob(ExecuteCopyDTO dto)
    {
        var sourceBlobClient = new BlobClient(_azureStorageConnString, dto.SourceContainer, dto.SourceBlob);
        var destBlobClient = new BlobClient(_azureStorageConnString, dto.DestinationContainer, dto.DestinationBlob);
        destBlobClient.StartCopyFromUriAsync(sourceBlobClient.Uri);
    }

    public void AddBlobMetadata(AddMetadataDTO dto)
    {
        var blobClient = new BlobClient(_azureStorageConnString, dto.Container, dto.Blob);
        blobClient.SetMetadataAsync(dto.Metadata);
    }  
}
