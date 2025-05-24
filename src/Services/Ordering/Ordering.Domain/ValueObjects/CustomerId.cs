namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    private CustomerId(Guid value) => Value = value;
    
    public static CustomerId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Customer Id cannot be empty");
        }
        return new CustomerId(value);
    }

    public Guid Value { get; } 
}