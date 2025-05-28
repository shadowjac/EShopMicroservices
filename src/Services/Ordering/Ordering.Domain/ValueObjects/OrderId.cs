namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    private OrderId() { }
    private OrderId(Guid value) => Value = value;
    
    public static OrderId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Order Id cannot be empty");
        }
        return new OrderId(value);
    }

    public Guid Value { get; } 
}