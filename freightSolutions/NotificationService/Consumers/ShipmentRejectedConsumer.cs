using Contracts;
using MassTransit;

namespace NotificationService.Consumers;

public class ShipmentRejectedConsumer : IConsumer<IShipmentRejected>
{
    public Task Consume(ConsumeContext<IShipmentRejected> context)
    {
        var shipment = context.Message;
        Console.WriteLine($"Shipment Rejected: {shipment.TrackingNumber}, Reason: {shipment.Reason}");
        return Task.CompletedTask;
    }
}