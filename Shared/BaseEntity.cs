namespace Shared;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAtUtc = DateTime.UtcNow;
    }

    protected BaseEntity(
        Guid id,
        DateTime createdAtUtc,
        DateTime? deletedAtUtc,
        bool isDeleted)
    {
        Id = id;
        CreatedAtUtc = createdAtUtc;
        DeletedAtUtc = deletedAtUtc;
        IsDeleted = isDeleted;
    }

    public Guid Id { get; protected init; }
    public DateTime CreatedAtUtc { get; protected init; }

    public DateTime? DeletedAtUtc { get; protected set; }
    public bool IsDeleted { get; protected set; }

    public void DeleteEntity()
    {
        if (IsDeleted)
        {
            return;
        }

        DeletedAtUtc = DateTime.UtcNow;
        IsDeleted = true;
    }
}