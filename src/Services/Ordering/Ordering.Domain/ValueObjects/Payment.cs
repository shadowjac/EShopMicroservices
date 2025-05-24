namespace Ordering.Domain.ValueObjects;

public record Payment
{
    protected Payment()
    {
        
    }
    private Payment(string? CardName,
        string CardNumber,
        string Expiration,
        string Cvv,
        string PaymentMethod)
    {
        this.CardName = CardName;
        this.CardNumber = CardNumber;
        this.Expiration = Expiration;
        CVV = Cvv;
        this.PaymentMethod = PaymentMethod;
    }

    public string? CardName { get; init; } = default!; 
    public string CardNumber { get; init; } = default!;
    public string Expiration { get; init; } = default!;
    public string CVV { get; init; } =  default!;  
    public string PaymentMethod { get; init; } = default!;
    
    public static Payment Of(string cardName,
        string cardNumber,
        string expiration,
        string cvv,
        string paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}