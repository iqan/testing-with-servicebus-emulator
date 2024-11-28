using Azure.Data.Tables;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace EndToEndTests;

public class FunctionAppFixture : IDisposable
{
    public FunctionAppFixture()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("testSettings.json")
            .AddEnvironmentVariables()
            .Build();

        var serviceBusClient = new ServiceBusClient(configuration["ServiceBusConnectionString"]);
        ServiceBusSender = serviceBusClient.CreateSender(configuration["TopicName"]);
        
        TableClient = new TableClient(configuration["StorageConnectionString"], configuration["TableName"]);
    }

    public ServiceBusSender ServiceBusSender { get; }

    public TableClient TableClient { get; }

    public void Dispose()
    {
    }
}