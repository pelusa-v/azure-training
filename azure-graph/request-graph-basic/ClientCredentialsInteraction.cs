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
        var clientId = "";
        var tenantId = "";
        var clientSecret = "";

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
