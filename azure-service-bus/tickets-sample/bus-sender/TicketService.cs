using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace bus_sender;

public class TicketService
{
    private readonly string _connString;
    private readonly string _queueName;

    public TicketService(string connString, string queueName)
    {
        _connString = connString;
        _queueName = queueName;
    }

    public async Task SendTicketAsync(string name)
    {
        var client = new ServiceBusClient(_connString);
        var sender = client.CreateSender(_queueName);  // queue or topic name

        try
        {
            var ticket = new Ticket(name);
            var message = new ServiceBusMessage(JsonSerializer.Serialize(ticket));
            await sender.SendMessageAsync(message);
        }
        finally
        {
            await sender.DisposeAsync();
            await client.DisposeAsync();
        }
    }
}

public class Ticket
{
    public string Name { get; set; }
    public string Message { get; set; }

    public Ticket(string name)
    {
        Name = name;
        Message = $"Ticket generated, {name}!";
    }
}
