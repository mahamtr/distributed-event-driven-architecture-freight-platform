using MassTransit;
using Contracts;

namespace PaymentService.Consumers;

public class RefundPaymentConsumer : IConsumer<RefundPayment>
{
    public async Task Consume(ConsumeContext<RefundPayment> context)
    {
        Console.WriteLine($"Refunding payment for OrderId: {context.Message.OrderId}");

        // Simulate refunding payment
        await Task.Delay(100);
    }
}