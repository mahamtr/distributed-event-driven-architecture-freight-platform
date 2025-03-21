using Contracts;
using MassTransit;

namespace NotificationService.Consumers;

public class ShipmentClearedConsumer : IConsumer<IShipmentCleared>
{
    public Task Consume(ConsumeContext<IShipmentCleared> context)
    {
        var shipment = context.Message;
        Console.WriteLine($"Shipment Cleared: {shipment.TrackingNumber}");
        return Task.CompletedTask;
    }
}