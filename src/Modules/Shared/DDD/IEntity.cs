namespace Shared.DDD;

public interface IEntity
{
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }
    public Guid? LastModifiedBy { get; set; }
}

public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}