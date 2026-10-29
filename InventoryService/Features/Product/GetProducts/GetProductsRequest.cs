using System.ComponentModel.DataAnnotations;

namespace InventoryService.Features.Product.GetProducts;

public record GetProductsRequest(
    [Required, Range(1, int.MaxValue)] int PageSize,
    [Required, Range(1, int.MaxValue)] int Page);