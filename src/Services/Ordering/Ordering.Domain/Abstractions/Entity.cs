namespace Ordering.Domain.Abstractions;

public abstract class Entity<TId> : IEntity<TId> where TId : notnull
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public TId Id { get; set; } = default!;
}