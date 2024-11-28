using Azure.Data.Tables;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace EndToEndTests;

public class FunctionAppFixture : IDisposable
{
    public FunctionAppFixture()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("testSettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        var serviceBusClient = new ServiceBusClient(Configuration["ServiceBusConnectionString"]);
        ServiceBusSender = serviceBusClient.CreateSender(Configuration["TopicName"]);
        
        TableClient = new TableClient(Configuration["StorageConnectionString"], Configuration["TableName"]);
    }
    
    public IConfiguration Configuration { get; }
    
    public ServiceBusSender ServiceBusSender { get; }
    
    public TableClient TableClient { get; }
    
    public void Dispose()
    {
    }
}