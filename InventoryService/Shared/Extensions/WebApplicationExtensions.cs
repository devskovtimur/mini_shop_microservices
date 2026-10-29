using InventoryService.Features.Product.GetProducts;
using InventoryService.Features.Reservation.GetReservation;

namespace InventoryService.Shared.Extensions;

public static class WebApplicationExtensions
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        app.MapGetProducts();
    }
    
    public static void MapReservationEndpoints(this WebApplication app)
    {
        app.MapGetReservation();
    }
}