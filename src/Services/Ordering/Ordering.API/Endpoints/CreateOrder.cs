﻿using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);

public record CreateOrderResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders",
                async (CreateOrderRequest request,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<CreateOrderCommand>();
                    var result = await sender.Send(command, cancellationToken);

                    var response = result.Adapt<CreateOrderResponse>();

                    return Results.Created($"/orders/{response.Id}", response);
                })
        .WithName("CreateOrder")
        .WithTags("Orders")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .WithSummary("Creates a new order")
        .WithDescription("Creates a new order in the system. The order details must be provided in the request body.");
    }
}