using MassTransit;
using Contracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);

// Add MassTransit with MongoDB persistence
builder.Services.AddMassTransitConfiguration(cfg =>
{
    cfg.AddSagaStateMachine<OrderProcessingSaga, OrderProcessingSagaState>()
        .MongoDbRepository(r =>
        {
            r.Connection = "mongodb://mongodb:27017";
            r.DatabaseName = "sagaDb";
        });
});

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/orchestrator/submit-order", async (IPublishEndpoint publishEndpoint, decimal amount) =>
{
    var orderSubmittedEvent = new OrderSubmitted(Guid.NewGuid(),amount);

    await publishEndpoint.Publish(orderSubmittedEvent);

    return Results.Ok("Order submitted and saga started.");
});

app.Run();
