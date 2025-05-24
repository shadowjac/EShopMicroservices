namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    private OrderItemId(Guid value) => Value = value;
    
    public static OrderItemId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Order Item Id cannot be empty");
        }
        return new OrderItemId(value);
    }
    
    // add implicit operator to convert from guid to OrderItemId
    public static implicit operator OrderItemId(Guid value) => Of(value);

    public Guid Value { get; } 
}