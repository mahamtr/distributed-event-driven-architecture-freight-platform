using MassTransit;
using Contracts;

namespace OrderService.Consumers;

public class CreateOrderConsumer : IConsumer<CreateOrder>
{
    public async Task Consume(ConsumeContext<CreateOrder> context)
    {
        Console.WriteLine($"Creating order in DB for OrderId: {context.Message.OrderId}");

        // Simulate order creation in DB
        await Task.Delay(100);

        await context.Publish(new OrderCreated(context.Message.OrderId));
    }
}