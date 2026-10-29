using Shared;
using Shared.Abstractions;

namespace InventoryService.Features.Product.Abstractions;

public interface IProductRepository : IGenericRepository<Product, Guid>
{
    Task<IEnumerable<Product>> GetAllProducts(int pageSize, int page, CancellationToken ct = default);
    Task<Result> UpdateQuantity(Product entity, CancellationToken ct = default);
}