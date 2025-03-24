using Contracts;
using OrderService.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransitConfiguration(cfg =>
{
    cfg.AddConsumer<CreateOrderConsumer>();
    cfg.AddConsumer<UpdateCancelOrderConsumer>();
});

var host = builder.Build();
host.Run();
