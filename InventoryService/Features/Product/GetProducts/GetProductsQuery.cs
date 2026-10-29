using InventoryService.Features.Product.Dtos;
using Shared.Mediator;

namespace InventoryService.Features.Product.GetProducts;

public record GetProductsQuery(int PageSize, int Page) : IQuery<IEnumerable<ProductDto>>;