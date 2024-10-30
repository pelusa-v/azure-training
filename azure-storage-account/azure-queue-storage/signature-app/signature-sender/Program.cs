using System.Text.Json;
using Azure.Storage.Queues;

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

var connString = "";
var queueName = "";

app.MapGet("/signature/send/{name}", (string name) =>
{
    QueueClient queueClient = new QueueClient(connString, queueName);
    if (queueClient.Exists())
    {
        var signature = new Signature
        {
            Name = name,
            DocName = "Contract"
        };
        var message = JsonSerializer.Serialize(signature);
        // Send a message to the queue
        queueClient.SendMessage(message);
    }
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

public class Signature
{
    public required string Name { get; set; }
    public required string DocName { get; set; }
}