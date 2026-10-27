using Shared.Abstractions;

namespace InventoryService.Features.Product.Abstractions;

public interface IProductRepository : IGenericRepository<Product, Guid>
{
    
}