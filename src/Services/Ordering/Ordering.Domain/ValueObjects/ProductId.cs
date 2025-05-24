namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    private ProductId(Guid value) => Value = value;
    
    public static ProductId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Product Id cannot be empty");
        }
        return new ProductId(value);
    }

    public Guid Value { get; }
}