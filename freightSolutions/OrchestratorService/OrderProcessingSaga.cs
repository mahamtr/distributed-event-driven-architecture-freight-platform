using MassTransit;
using Contracts;

public class OrderProcessingSaga : MassTransitStateMachine<OrderProcessingSagaState>
{
    public OrderProcessingSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderSubmitted, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => OrderCreated, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => InventoryReserved, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => PaymentProcessed, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => CancelOrder, x => x.CorrelateById(context => context.Message.OrderId));

        Initially(
            When(OrderSubmitted)
                .Then(_ => Console.WriteLine("OrderSubmitted received"))
                .Then(context => context.Saga.OrderId = context.Message.OrderId)
                .Then(context => context.Saga.Amount = context.Message.Amount)
                .Publish(context => new CreateOrder(context.Saga.OrderId))
                .TransitionTo(OrderSubmittedState));

        During(OrderSubmittedState,
            When(OrderCreated)
                .Then(context => context.Saga.IsOrderCreated = true)
                .Publish(context => new InventoryReserve(context.Saga.OrderId))
                .TransitionTo(OrderCreatedState));

        During(OrderCreatedState,
            When(InventoryReserved)
                .Then(context => context.Saga.IsStockReserved = true)
                .Publish(context => new ProcessPayment(context.Saga.OrderId))
                .TransitionTo(InventoryReservedState));

        During(InventoryReservedState,
            When(PaymentProcessed)
                .Then(context => context.Saga.IsPaymentProcessed = true)
                .Then(_ => Console.WriteLine("Order saga finished successfully."))
                .TransitionTo(CompletedState));

        DuringAny(
            When(CancelOrder)
                .Then(context =>
                {
                    if (context.Saga.IsStockReserved)
                    {
                        context.Publish(new ReleaseInventory(context.Saga.OrderId));
                    }
                    if (context.Saga.IsPaymentProcessed)
                    {
                        context.Publish(new RefundPayment(context.Saga.OrderId));
                    }
                    context.Publish(new UpdateCancelOrder(context.Saga.OrderId, "Cancelled"));
                })
                .Finalize());

        SetCompletedWhenFinalized();
    }

    public State OrderSubmittedState { get; private set; }
    public State OrderCreatedState { get; private set; }
    public State InventoryReservedState { get; private set; }
    public State CompletedState { get; private set; }

    public Event<OrderSubmitted> OrderSubmitted { get; private set; }
    public Event<OrderCreated> OrderCreated { get; private set; }
    public Event<InventoryReserved> InventoryReserved { get; private set; }
    public Event<PaymentProcessed> PaymentProcessed { get; private set; }
    public Event<CancelOrder> CancelOrder { get; private set; }
}

public class OrderProcessingSagaState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = string.Empty;
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public bool IsOrderCreated { get; set; }
    public bool IsStockReserved { get; set; }
    public bool IsPaymentProcessed { get; set; }
    public int Version { get; set; } 
}
