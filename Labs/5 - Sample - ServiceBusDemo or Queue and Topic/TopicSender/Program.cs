using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

class Program
{
    static string connectionString = "Endpoint=sb://dssdemo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=U3iNWNUvmAZQgZgtQKSlVZbsAW+gFh/KuqGaaIEQZuU=";
    static string topicName = "courses";
    static ServiceBusClient client;
    static ServiceBusSender sender;
    static async Task Main()
    {
        client = new ServiceBusClient(connectionString);
        sender = client.CreateSender(topicName);
        while (true)
        {
            Console.WriteLine("Enter Course Name (exit to terminate): ");
            string m = Console.ReadLine();
            if (m == "exit")
                break;
            var msg = new ServiceBusMessage(m);
            Console.WriteLine("Enter Author: ");
            string author = Console.ReadLine();
            msg.ApplicationProperties.Add("Author", author);

            await sender.SendMessageAsync(msg);
            Console.WriteLine("Sent...");
        }
        await sender.DisposeAsync();
        await client.DisposeAsync();
    }
}
