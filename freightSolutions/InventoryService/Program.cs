using Contracts;
using InventoryService.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransitConfiguration(cfg =>
{
    cfg.AddConsumer<InventoryReserveConsumer>();
    cfg.AddConsumer<ReleaseInventoryConsumer>();
});

var host = builder.Build();
host.Run();
