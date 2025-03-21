using Contracts;
using MassTransit;

namespace CustomsService.Consumers;

public class ShipmentUpdatedConsumer : IConsumer<IShipmentUpdated>
{
    public async Task Consume(ConsumeContext<IShipmentUpdated> context)
    {
        var shipment = context.Message;
        Console.WriteLine($"Shipment update received: {shipment.TrackingNumber}, Status: {shipment.Status}");
        if (shipment.Status == "In Transit")
        {
            await context.Publish<IShipmentCleared>(new
            {
                ShipmentId = shipment.ShipmentId,
                TrackingNumber = shipment.TrackingNumber
            });
        }
        else
        {
            await context.Publish<IShipmentRejected>(new
            {
                ShipmentId = shipment.ShipmentId,
                TrackingNumber = shipment.TrackingNumber,
                Reason = "Customs clearance failed"
            });
        }
    }
}