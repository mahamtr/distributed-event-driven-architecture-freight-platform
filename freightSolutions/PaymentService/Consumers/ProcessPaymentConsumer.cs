using MassTransit;
using Contracts;

namespace PaymentService.Consumers;

public class ProcessPaymentConsumer : IConsumer<ProcessPayment>
{
    public async Task Consume(ConsumeContext<ProcessPayment> context)
    {
        Console.WriteLine($"Processing payment for OrderId: {context.Message.OrderId}");

        // Simulate payment processing
        bool paymentSuccess = true; // Change this to simulate payment success or failure

        if (paymentSuccess)
        {
            await context.Publish(new PaymentProcessed(context.Message.OrderId));
        }
        else
        {
            Console.WriteLine($"Payment failed for OrderId: {context.Message.OrderId}");
            await context.Publish(new CancelOrder(context.Message.OrderId));
        }
    }
}