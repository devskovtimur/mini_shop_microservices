using InventoryService.Features.Reservation;
using InventoryService.Features.Reservation.Abstractions;
using InventoryService.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.SetupMongoDb();
builder.Services.AddScoped<IReservationService, ReservationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapProductsEndpoints();
app.MapReservationEndpoints();

app.Run();