using BuildingBlocks.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.EventHandlers.Integrations;

public class BasketCheckoutEventHandler : IConsumer<BasketCheckoutEvent>
{
    private readonly ISender _sender;
    private readonly ILogger<BasketCheckoutEventHandler> _logger;

    public BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        _logger.LogInformation("Integration event handled {IntegrationEvent}", context.Message.GetType().Name);
        
        var command = MapToCreateOrderCommand(context.Message);
        await _sender.Send(command, context.CancellationToken);
        
    }

    private static CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress,
            message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.Cvv,
            message.PaymentMethod);
        var orderId = Guid.NewGuid();
        
        var orderDto = new OrderDto(
            orderId,
            message.CustomerId,
            message.UserName,
            addressDto,
            addressDto,
            paymentDto,
            Status: OrderStatus.Pending,
            OrderItems: [
                new OrderItemDto(orderId, new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D3"), 2, 500),
                new OrderItemDto(orderId, new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D4"), 1, 400)
            ]);
        
        return new CreateOrderCommand(orderDto);
    }
}