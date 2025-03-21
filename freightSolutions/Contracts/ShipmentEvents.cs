namespace Contracts
{
    public interface IShipmentUpdated
    {
        Guid ShipmentId { get; }
        string TrackingNumber { get; }
        string Status { get; } // Example: "In Transit", "Delivered"
    }

    public interface IShipmentCleared
    {
        Guid ShipmentId { get; }
        string TrackingNumber { get; }
    }

    public interface IShipmentRejected
    {
        Guid ShipmentId { get; }
        string TrackingNumber { get; }
        string Reason { get; }
    }
}