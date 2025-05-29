﻿namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);
        
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new CreateOrderResult(order.Id.Value);
    }

    private static Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.ZipCode);
        
        var billingAddress = Address.Of(orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.ZipCode);

        var newOrder = Order.Create(
            orderId: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: shippingAddress,
            billingAddress: billingAddress,
            payment: Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration,
                orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod));

        foreach (var item in orderDto.OrderItems)
        {
            newOrder.AddItem(ProductId.Of(item.ProductId), item.Price, item.Quantity);
        }

        return newOrder;
    }
}