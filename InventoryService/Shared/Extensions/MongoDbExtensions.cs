using MongoDB.Driver;
using Shared;

namespace InventoryService.Shared.Extensions;

public static class MongoFilterExtensions
{
    public static FilterDefinition<T> GetFilterById<T>(this FilterDefinitionBuilder<T> builder, Guid id,
        bool includeDeleted = false)
        where T : BaseEntity
    {
        var filter = builder.Eq(x => x.Id, id);

        if (includeDeleted)
        {
            filter &= builder.NotDeletedOnly();
        }

        return filter;
    }

    public static FilterDefinition<T> NotDeletedOnly<T>(this FilterDefinitionBuilder<T> builder)
        where T : BaseEntity
    {
        return builder.Or(
            builder.Eq(x => x.IsDeleted, false),
            builder.Exists(x => x.IsDeleted, false)
        );
    }
}