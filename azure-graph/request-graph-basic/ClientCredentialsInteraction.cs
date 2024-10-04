namespace request_graph_basic;

using Azure.Identity;
using Microsoft.Graph;

public static class ClientCredentialsInteraction
{
    public static async Task Execute()
    {
        // The client credentials flow requires that you request the
        // /.default scope, and pre-configure your permissions on the
        // app registration in Azure. An administrator must grant consent
        // to those permissions beforehand.

        var scopes = new[] { "https://graph.microsoft.com/.default" };

        // Values from app registration
        var clientId = "1bb1b054-6e4d-4d43-977e-6f2c33ce452a";
        var tenantId = "27b8e21b-adae-4ec5-8fe0-85042b64d2d4";
        var clientSecret = "ulB8Q~0B68gDsNCYSSS3t5FPNsFmDvrtTKVDvc4v";

        // using Azure.Identity;
        var options = new ClientSecretCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
        };

        // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
        var clientSecretCredential = new ClientSecretCredential(
            tenantId, clientId, clientSecret, options);

        var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

        var users = await graphClient.Users.GetAsync();
        Console.WriteLine("");
    }
}
