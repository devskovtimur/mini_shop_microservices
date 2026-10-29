using MassTransit;
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();