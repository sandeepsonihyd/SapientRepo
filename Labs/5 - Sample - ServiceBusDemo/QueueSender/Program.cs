using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
class Program
{
    static string connectionString = "Endpoint=sb://dssdemosb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cDSyPSxHTssFK/DwNk93Rhjyk9D0y4G4Ob0uRHZu3Ko=";
    static string queueName = "demo-queue";

    static async Task Main()
    {
        ServiceBusClient client = new ServiceBusClient(connectionString);
        ServiceBusSender sender = client.CreateSender(queueName);
       //using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
       // sender.SendMessagesAsync(messageBatch);
        while (true)
        {
            Console.WriteLine("Enter Message (exit to terminate): ");
            string m = Console.ReadLine();
            if (m == "exit")
                break;
            var msg = new ServiceBusMessage(m);
            //msg.SessionId = "MySession";
            msg.ApplicationProperties.Add("Author", "sandeep");
            msg.ApplicationProperties.Add("CreatedAt", DateTime.Now);
            msg.ApplicationProperties.Add("Source", "DemoApp");
            
            //msg.TimeToLive = new TimeSpan(0, 0, 5);
            msg.MessageId = m;
            await sender.SendMessageAsync(msg);
            Console.WriteLine("Sent: " + msg.MessageId);
        }
        await sender.DisposeAsync();
        await client.DisposeAsync();
    }
}
