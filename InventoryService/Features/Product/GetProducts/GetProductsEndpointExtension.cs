using InventoryService.Features.Product.Dtos;
using MediatR;
using Shared.Extensions;

namespace InventoryService.Features.Product.GetProducts;

public static class GetProductsEndpointExtension
{
    public static void MapGetProducts(this WebApplication app)
    {
        app.MapGet("api/products/{pageSize:int}/{page:int}",
                async (GetProductsRequest request, ISender sender,
                    CancellationToken ct) =>
                {
                    var result = await sender.Send(
                        new GetProductsQuery(request.PageSize, request.Page), ct);

                    return result.ToHttpResponse();
                })
            .WithName("GetProducts")
            .WithDescription("Get products")
            .Produces<IEnumerable<ProductDto>>();
    }
}