using System;

namespace practice_integration_api.Application;

public class AddMetadataDTO
{
    public string Container { get; set; }
    public string Blob { get; set; }
    public IDictionary<string, string> Metadata { get; set; }
}
