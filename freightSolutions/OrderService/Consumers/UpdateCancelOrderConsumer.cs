using MassTransit;
using Contracts;

namespace OrderService.Consumers;

public class UpdateCancelOrderConsumer : IConsumer<UpdateCancelOrder>
{
    public async Task Consume(ConsumeContext<UpdateCancelOrder> context)
    {
        Console.WriteLine($"Updating order status to '{context.Message.Status}' for OrderId: {context.Message.OrderId}");

        // Simulate updating order status in DB
        await Task.Delay(100);
    }
}