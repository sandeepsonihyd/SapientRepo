using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Primitives;
using System.Collections.Generic;

class Program
{
    private const string ehubNamespaceConnectionString = "Endpoint=sb://dsdemo-eventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=jeUBqrJ+B7dtI0CeRfoWKwGKaa7CLH/gb+AEhOl4y8o=";
    private const string eventHubName = "demohub2";
    private const string blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=dsdemostorage123;AccountKey=VG3p49BDiHlYn2szJuRHJXsW/RTYf2JmXAHTm+MwUWnNUH3vuSCqaWLkmFpyhMkHOxbotCIgvE8a+AStf9A47w==;EndpointSuffix=core.windows.net";
    private const string blobContainerName = "ehcon2";


    static async Task Main()
    {
        // Read from the default consumer group: $Default
        string consumerGroup = "consumer2";
        // Create a blob container client that the event processor will use 
        BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);
        // Create an event processor client to process events in the event hub
        EventProcessorClient processor = new EventProcessorClient(storageClient, consumerGroup, ehubNamespaceConnectionString, eventHubName);
        // Register handlers for processing events and handling errors
        processor.ProcessEventAsync += ProcessEventHandler;
        processor.ProcessErrorAsync += ProcessErrorHandler;

        // Start the processing
        await processor.StartProcessingAsync();
        Console.WriteLine("Press Enter to stop...");
        Console.ReadLine();
        // Stop the processing
        await processor.StopProcessingAsync();


        // Reading Data from a specific partition.
        //var connectionString = "<< EVENT HUB CONNECTION STRING FROM PORTAL >>";
        //var consumerGroup = "$DEFAULT";
        //var partitionId = "0";
        //var initialPosition = EventPosition.FromOffset(123);
        //var partitionReceiver = new PartitionReceiver(consumerGroup, partitionId, initialPosition, connectionString);
        //IEnumerable<EventData> batch = await partitionReceiver.ReceiveBatchAsync(100);
    }
    static async Task ProcessEventHandler(ProcessEventArgs eventArgs)
    {
        // Write the body of the event to the console window
        Console.WriteLine("\tRecevied event: {0} - {1}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()),eventArgs.Partition.PartitionId);
        // Update checkpoint in the blob storage so that the app receives only new events the next time it's run
        await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
    }
    static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
    {
        // Write details about the error to the console window
        Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
        Console.WriteLine(eventArgs.Exception.Message);
        return Task.CompletedTask;
    }
}
