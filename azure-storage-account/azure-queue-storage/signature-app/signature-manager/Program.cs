using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

var connString = "";
var queueName = "";

QueueClient queueClient = new QueueClient(connString, queueName);

if (queueClient.Exists())
{ 
    // Peek at the next message
    QueueMessage[] peekedMessage = queueClient.ReceiveMessages();

    foreach (QueueMessage message in peekedMessage)
    {
        Console.WriteLine($"Message: {message.MessageText}");
    }
}