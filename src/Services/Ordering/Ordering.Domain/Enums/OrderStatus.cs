namespace Ordering.Domain.Enums;

public enum OrderStatus
{
    None,
    Draft = 1,
    Pending,
    Completed,
    Cancelled
}