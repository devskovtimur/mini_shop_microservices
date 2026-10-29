using AutoMapper;
using InventoryService.Features.Product.Abstractions;
using InventoryService.Features.Product.Dtos;
using Shared;
using Shared.Mediator;

namespace InventoryService.Features.Product.GetProducts;

public class GetProductsHandler(
    IProductRepository productRepository,
    IMapper mapper)
    : IQueryHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<Result<IEnumerable<ProductDto>>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllProducts(
            pageSize: request.PageSize,
            page: request.Page,
            ct: cancellationToken);

        var productDtos = products.Select(mapper.Map<ProductDto>);
        return Result<IEnumerable<ProductDto>>.Success(productDtos);
    }
}