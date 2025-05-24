namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice => OrderItems.Sum(item => item.Price * item.Quantity);

    public static Order Create(OrderId orderId,
        CustomerId customerId,
        OrderName orderName,
        Address shippingAddress,
        Address billingAddress,
        Payment payment)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        ArgumentNullException.ThrowIfNull(customerId);
        ArgumentNullException.ThrowIfNull(orderName);
        ArgumentNullException.ThrowIfNull(shippingAddress);
        ArgumentNullException.ThrowIfNull(billingAddress);
        ArgumentNullException.ThrowIfNull(payment);

        var order = new Order
        {
            Id = orderId,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };

        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress,
        Address billingAddress,
        Payment payment,
        OrderStatus status)
    {
        ArgumentNullException.ThrowIfNull(orderName);
        ArgumentNullException.ThrowIfNull(shippingAddress);
        ArgumentNullException.ThrowIfNull(billingAddress);
        ArgumentNullException.ThrowIfNull(payment);

        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void AddOrderItem(ProductId productId, decimal price, int quantity)
    {
        ArgumentNullException.ThrowIfNull(productId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        var orderItem = new OrderItem(Id, productId, price, quantity);
        _orderItems.Add(orderItem);
    }

    public void RemoveOrderItem(ProductId productId)
    {
        ArgumentNullException.ThrowIfNull(productId);

        var orderItem = _orderItems.FirstOrDefault(item => item.ProductId == productId);
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}