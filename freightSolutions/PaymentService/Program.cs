using Contracts;
using PaymentService.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransitConfiguration(cfg =>
{
    cfg.AddConsumer<ProcessPaymentConsumer>();
    cfg.AddConsumer<RefundPaymentConsumer>();
});

var host = builder.Build();
host.Run();
