using System;

namespace practice_integration_api.Application;

public class ExecuteCopyDTO
{
    public string SourceContainer { get; set; }
    public string SourceBlob { get; set; }
    public string DestinationContainer { get; set; }
    public string DestinationBlob { get; set; }
}
