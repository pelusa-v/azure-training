// See https://aka.ms/new-console-template for more information
using Azure.Identity;
using Azure.Messaging.EventGrid;

Console.WriteLine("Hello, World!");


string topicEndpoint = "https://testtopicjp.eastus-1.eventgrid.azure.net/api/events"; // e.g., https://<your-topic>.<region>-1.eventgrid.azure.net/api/events
string tenantId = "27b8e21b-adae-4ec5-8fe0-85042b64d2d4";
string clientId = "";
string clientSecret = "";

// Create an EventGridPublisherClient using the topic's endpoint and key
var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
var client = new EventGridPublisherClient(new Uri(topicEndpoint), credential);

// Create the event data
var eventData = new EventGridEvent(
    subject: "Grid.Sender.Local.Test",
    eventType: "MyApp.Models.SampleEvent",
    dataVersion: "1.0",
    data: new
    {
        Id = Guid.NewGuid(),
        Name = "Sample Event",
        Message = "This is a test message from Event Grid"
    });
    
// Publish the event
var res = await client.SendEventAsync(eventData);
Console.WriteLine("Event Published");