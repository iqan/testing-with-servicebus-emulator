using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FunctionApp.Models;

namespace FunctionApp;

public class ServiceBusTrigger
{
    private readonly ILogger<ServiceBusTrigger> _logger;

    public ServiceBusTrigger(ILogger<ServiceBusTrigger> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ServiceBusTrigger))]
    [TableOutput("ToDoTable", Connection = "StorageConnection")]
    public ToDoTableData Run(
        [ServiceBusTrigger("mytopic", "mysubscription", Connection = "ServiceBusConnection")]
        ToDoMessageData message)
    {
        return new ToDoTableData
        {
            Task = message.Task,
            IsCompleted = message.IsCompleted ?? false,
            PartitionKey = DateTime.UtcNow.ToString("yyyyMMdd"),
            RowKey = Guid.NewGuid().ToString(),
            Timestamp = DateTimeOffset.UtcNow
        };
    }
}