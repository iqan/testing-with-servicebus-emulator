using Azure;
using Azure.Data.Tables;

namespace EndToEndTests.Models;

public class ToDoTableData : ITableEntity
{
    public string Task { get; set; }
    public bool IsCompleted { get; set; }

    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}