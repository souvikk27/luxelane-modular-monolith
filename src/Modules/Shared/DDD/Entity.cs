namespace Shared.DDD;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime CreatedBy { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public DateTime LastModifiedBy { get; set; }

    protected Entity()
    {
    }

    protected Entity(T id)
    {
        Id = id;
    }
}