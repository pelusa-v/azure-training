using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/appsecrets/{secretname}", (string secretname) => 
{
    // var _tenantId = "";
    // var _clientId = "";
    // var _clientSecret = "";
    // var credentials = new ClientSecretCredential(_tenantId, _clientId, _clientSecret);
    // var client = new SecretClient(new Uri("kvuri"), credentials);
    var client = new SecretClient(new Uri("kvuri"), new DefaultAzureCredential());
    var secret = client.GetSecret(secretname);
    return secret.Value.Value;
})
.WithName("GetSecrets")
.WithOpenApi();


app.Run();