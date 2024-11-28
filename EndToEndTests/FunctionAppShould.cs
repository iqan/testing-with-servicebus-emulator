using Azure.Messaging.ServiceBus;
using EndToEndTests.Models;
using FluentAssertions;

namespace EndToEndTests;

public class FunctionAppShould : IClassFixture<FunctionAppFixture>
{
    private readonly FunctionAppFixture _fixture;

    public FunctionAppShould(FunctionAppFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SaveToDoItemToTable_New()
    {
        var message = new ToDoMessageData
        {
            Task = $"Write Sample App | {Guid.NewGuid().ToString("D")}"
        };
        
        await _fixture.ServiceBusSender.SendMessageAsync(message.ToServiceBusMessage());

        var results = _fixture.TableClient.QueryAsync<ToDoTableData>($"Task eq '{message.Task}'");
        await foreach (var entity in results)
        {
            entity.Should().NotBeNull();
            entity.IsCompleted.Should().BeFalse();
            await _fixture.TableClient.DeleteEntityAsync(entity);
        }
    }
}