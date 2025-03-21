using Contracts;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add MassTransit configuration
builder.Services.AddMassTransitConfiguration();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Define an endpoint to publish the IShipmentUpdated event
app.MapPost("/shipments/update", async (IPublishEndpoint publishEndpoint, ShipmentUpdateRequest request) =>
{
    var shipmentUpdatedEvent = new
    {
        ShipmentId = Guid.NewGuid(),
        TrackingNumber = request.TrackingNumber,
        Status = request.Status
    };

    await publishEndpoint.Publish<IShipmentUpdated>(shipmentUpdatedEvent);

    return Results.Ok("Shipment update event published.");
});

app.Run();

record ShipmentUpdateRequest(string TrackingNumber, string Status);
