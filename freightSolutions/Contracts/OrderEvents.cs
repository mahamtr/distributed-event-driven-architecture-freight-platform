namespace Contracts
{
    public record OrderSubmitted(Guid OrderId, decimal Amount);

    public record CreateOrder(Guid OrderId);

    public record OrderCreated(Guid OrderId);

    public record InventoryReserve(Guid OrderId);

    public record InventoryReserved(Guid OrderId);

    public record ProcessPayment(Guid OrderId);

    public record PaymentProcessed(Guid OrderId);

    public record CancelOrder(Guid OrderId);

    public record ReleaseInventory(Guid OrderId);

    public record RefundPayment(Guid OrderId);

    public record UpdateCancelOrder(Guid OrderId, string Status);
}