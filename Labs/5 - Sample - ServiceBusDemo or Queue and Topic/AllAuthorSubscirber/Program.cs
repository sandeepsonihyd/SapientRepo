using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

class Program
{
    static string connectionString = "Endpoint=sb://dssdemo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=U3iNWNUvmAZQgZgtQKSlVZbsAW+gFh/KuqGaaIEQZuU=";
    static string topicName = "courses";
    static string subscriptionName = "AllAuthors";
    static ServiceBusClient client;
    static ServiceBusProcessor processor;

    static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();
        Console.WriteLine($"Received: {body} from subscription: {subscriptionName}");
        // complete the message. messages is deleted from the subscription. 
        await args.CompleteMessageAsync(args.Message);
    }
    static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }

    static async Task Main()
    {
        client = new ServiceBusClient(connectionString);
        var options = new ServiceBusProcessorOptions();
        options.ReceiveMode = ServiceBusReceiveMode.PeekLock;
        processor = client.CreateProcessor(topicName, subscriptionName, options);
        try
        {
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;
            await processor.StartProcessingAsync();
            Console.WriteLine("Wait for a minute and then press any key to end the processing");
            Console.ReadKey();
            Console.WriteLine("\nStopping the receiver...");
            await processor.StopProcessingAsync();
            Console.WriteLine("Stopped receiving messages");
        }
        finally
        {
            await processor.DisposeAsync();
            await client.DisposeAsync();
        }
    }
}
