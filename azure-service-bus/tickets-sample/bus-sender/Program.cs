using bus_sender;

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


app.MapPost("/ticket/send/{name}", async (string name) =>
{
    var connString = "Endpoint=sb://testbusjp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hBse56bUoTadRt2LQQVvwpmINxLSLHHvl+ASbPUhulA=";
    var queueName = "notifications";
    var service = new TicketService(connString, queueName);
    await service.SendTicketAsync(name);
})
.WithName("SendTicket")
.WithOpenApi();

app.Run();
