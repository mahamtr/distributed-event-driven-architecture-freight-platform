using Contracts;
using NotificationService.Consumers;

var builder = Host.CreateApplicationBuilder(args);

// Add MassTransit configuration with consumers
builder.Services.AddMassTransitConfiguration(cfg =>
{
    cfg.AddConsumer<ShipmentClearedConsumer>();
    cfg.AddConsumer<ShipmentRejectedConsumer>();
});

var app = builder.Build();
app.Run();
