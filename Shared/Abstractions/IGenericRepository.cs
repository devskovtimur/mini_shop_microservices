namespace Shared.Abstractions;

public interface IGenericRepository<T, IdType> where T : class where IdType : notnull
{
    Task<T?> GetById(IdType id, CancellationToken ct = default);
    Task<bool> Exists(IdType id, CancellationToken ct = default);

    Task Add(T entity, CancellationToken ct = default);
    Task<Result> SoftRemove(T entity, CancellationToken ct = default);
}