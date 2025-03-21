using Contracts;
using CustomsService.Consumers;

var builder = Host.CreateApplicationBuilder(args);

// Add MassTransit configuration with consumers
builder.Services.AddMassTransitConfiguration(cfg =>
{
    cfg.AddConsumer<ShipmentUpdatedConsumer>();
});

var app = builder.Build();
app.Run();
