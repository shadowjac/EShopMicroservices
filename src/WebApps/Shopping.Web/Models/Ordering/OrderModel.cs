namespace Shopping.Web.Models.Ordering;

public record OrderModel(
    string UserName,
    Guid CustomerId,
    string OrderName,
    AddressModel ShippingAddress,
    AddressModel BillingAddress,
    PaymentModel Payment,
    OrderStatus Status,
    List<OrderItemModel> Items);

public record OrderItemModel(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal Price);

public record AddressModel(
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode);

public record PaymentModel(
    string CardName,
    string CardNumber,
    string Expiration,
    string CVV,
    int PaymentMethod);

public enum OrderStatus
{
    Draft = 1,
    Pending,
    Completed,
    Cancelled
}

// Wrapper classes
public record GetOrdersResponse(PaginatedResult<OrderModel> Orders);
public record GetOrderByNameResponse(IEnumerable<OrderModel> Orders);
public record GetOrderByCustomerResponse(OrderModel Order);
