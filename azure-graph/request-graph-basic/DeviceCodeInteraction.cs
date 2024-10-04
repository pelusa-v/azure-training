using Azure.Identity;
using Microsoft.Graph;

namespace request_graph_basic;

public static class DeviceCodeInteraction
{
    public static async Task Execute()
    {
        var scopes = new[] { "User.Read" };

        // Multi-tenant apps can use "common",
        // single-tenant apps must use the tenant ID from the Azure portal
        var tenantId = "27b8e21b-adae-4ec5-8fe0-85042b64d2d4";

        // Value from app registration
        var clientId = "1bb1b054-6e4d-4d43-977e-6f2c33ce452a";

        var options = new DeviceCodeCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            ClientId = clientId,
            TenantId = tenantId,
            // Callback function that receives the user prompt
            // Prompt contains the generated device code that user must
            // enter during the auth process in the browser
            DeviceCodeCallback = (code, cancellation) =>
            {
                Console.WriteLine("############ THIS IS THE DEVICE CODE CALLBACK ############");
                Console.WriteLine(code.Message);
                return Task.FromResult(0);
            },
        };

        var deviceCodeCredential = new DeviceCodeCredential(options);

        var graphClient = new GraphServiceClient(deviceCodeCredential, scopes);
        var user = await graphClient.Me
            .GetAsync();
        Console.WriteLine("");
    }
}
