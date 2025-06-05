using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IFeatureManager _featureManager;
    private readonly ILogger<OrderCreatedEventHandler> _logger;

    public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, IPublishEndpoint publishEndpoint,
        IFeatureManager featureManager)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _featureManager = featureManager;
    }

    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        if (await _featureManager.IsEnabledAsync("EnableOrderFulfillment"))
        {
            var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
            await _publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
        else
        {
            _logger.LogWarning("{FeatureKey} is disabled", "EnableOrderFulfillment");
        }
    }
}