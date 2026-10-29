using InventoryService.Features.Product.Abstractions;
using InventoryService.Shared.Extensions;
using MongoDB.Driver;
using Shared;

namespace InventoryService.Features.Product;

public class ProductRepository(IMongoDatabase database) : IProductRepository
{
    private readonly IMongoCollection<Product> _collection =
        database.GetCollection<Product>("products");

    public async Task<Product?> GetById(Guid id, CancellationToken ct = default)
    {
        var filter = Builders<Product>.Filter.GetFilterById(id);
        return await _collection.Find(filter).FirstOrDefaultAsync(ct);
    }

    public async Task<bool> Exists(Guid id, CancellationToken ct = default)
    {
        var product = await GetById(id, ct);
        return product is not null;
    }

    public async Task<IEnumerable<Product>> GetAllProducts(int pageSize, int page, CancellationToken ct = default)
    {
        var filter = Builders<Product>.Filter.NotDeletedOnly();
        var offset = (page - 1) * pageSize;
        
        return await _collection
            .Find(filter)
            .Skip(offset)
            .Limit(pageSize)
            .ToListAsync(ct);
    }

    public async Task Add(Product entity, CancellationToken ct = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: ct);
    }

    public async Task<Result> UpdateQuantity(Product entity, CancellationToken ct)
    {
        var filter = Builders<Product>.Filter.GetFilterById(entity.Id);
        var update = Builders<Product>.Update
            .Set(x => x.Quantity, entity.Quantity);

        var result = await _collection.UpdateOneAsync(filter, update, cancellationToken: ct);
        return GetResultDependingOnUpdateResult(result, entity.Id);
    }

    public Task<Result> SoftRemove(Product entity, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    private Result GetResultDependingOnUpdateResult(UpdateResult updateResult, Guid id)
    {
        if (updateResult.MatchedCount > 0)
        {
            return updateResult.ModifiedCount > 0
                ? Result.Success()
                : Result.Failure(Error.Conflict(
                    $"Product with {id} ID was found, " +
                    $"but nothing has changed because it already has the same values."));
        }

        return Result.Failure(
            Error.NotFound($"Product with {id} ID not found"));
    }
}