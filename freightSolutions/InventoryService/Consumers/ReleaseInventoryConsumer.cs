using MassTransit;
using Contracts;

namespace InventoryService.Consumers;

public class ReleaseInventoryConsumer : IConsumer<ReleaseInventory>
{
    public async Task Consume(ConsumeContext<ReleaseInventory> context)
    {
        Console.WriteLine($"Releasing inventory for OrderId: {context.Message.OrderId}");

        // Simulate releasing inventory
        await Task.Delay(100);
    }
}