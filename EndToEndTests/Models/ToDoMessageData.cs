using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace EndToEndTests.Models;

public class ToDoMessageData
{
  public string Task { get; set; }
  public bool? IsCompleted { get; set; }

  public ServiceBusMessage ToServiceBusMessage()
  {
    var content = JsonConvert.SerializeObject(this);
    return new ServiceBusMessage(content);
  }
}
