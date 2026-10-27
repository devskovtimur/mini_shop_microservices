using MassTransit;
using WalletService.Features.Transactions;
using WalletService.Features.Transactions.Abstractions;
using WalletService.Shared.Extensions;
using WalletService.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqSettings>(
    builder.Configuration.GetRequiredSection("RabbitMqSettings:HostAddress"));

builder.Services.AddMassTransit(c =>
{
    c.SetKebabCaseEndpointNameFormatter();

    c.UsingRabbitMq((context, cfg) =>
    {
        var rmqSettings = context.GetRequiredService<RabbitMqSettings>();

        cfg.Host(rmqSettings.Host, "/", hostConf =>
        {
            hostConf.Username(rmqSettings.User);
            hostConf.Password(rmqSettings.Password);
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.SetupDapper();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.MapWalletEndpoints();
app.MapTransactionEndpoints();

app.Run();