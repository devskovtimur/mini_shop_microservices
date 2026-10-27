using InventoryService.Features.Product.Abstractions;

namespace InventoryService.Features.Product;

public class ProductRepository : IProductRepository
{
    public Task<Product?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Exists(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Add(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Product entity)
    {
        throw new NotImplementedException();
    }
}