namespace Ordering.Domain.Abstractions;

public abstract class Entity<TId> : IEntity<TId>
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public required TId Id { get; set; }
}