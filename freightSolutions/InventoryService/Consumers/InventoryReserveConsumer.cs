using MassTransit;
using Contracts;

namespace InventoryService.Consumers;

public class InventoryReserveConsumer : IConsumer<InventoryReserve>
{
    public async Task Consume(ConsumeContext<InventoryReserve> context)
    {
        Console.WriteLine($"Reserving inventory for OrderId: {context.Message.OrderId}");

        bool inventoryAvailable = true;

        if (inventoryAvailable)
        {
            await context.Publish(new InventoryReserved(context.Message.OrderId));
        }
        else
        {
            Console.WriteLine($"No inventory available for OrderId: {context.Message.OrderId}");
            await context.Publish(new CancelOrder(context.Message.OrderId));
        }
    }
}